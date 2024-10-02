using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectBank.DataAcces.Data;
using ProjectBank.DataAcces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.DataAcces.Services.Accounts
{
    public class AccountService : IAccountService
    {
        private readonly DataContext _context;
        private readonly IValidator<Account> _validator;

        public AccountService(DataContext context, IValidator<Account> validator)
        {
            _context = context;
            _validator = validator;
        }

        public async Task<ActionResult<List<Account>>> Get(string? search, string? sortItem, string? sortOrder)
        {
            IQueryable<Account> accounts = _context.Account;

            if (!string.IsNullOrEmpty(search))
            {
                accounts = accounts.Where(n => n.Name.ToLower().Contains(search.ToLower()));
            }

            Expression<Func<Account, object>> selectorKey = sortItem?.ToLower() switch
            {
                "name" => account => account.Name,
                _ => account => account.Id,
            };

            accounts = sortOrder?.ToLower() == "desc"
                ? accounts.OrderByDescending(selectorKey)
                : accounts.OrderBy(selectorKey);

            List<Account> accountList = await accounts.ToListAsync();


            return accountList;
        }

        public async Task<Account> GetByLogin(string login)
        {
             var account = await _context.Account
                .Include(a => a.Customer)
                .Include(a => a.Cards)
                    .ThenInclude(c => c.SentTransactions)
                .Include(a => a.Cards)
                    .ThenInclude(c => c.ReceivedTransactions)
                .SingleOrDefaultAsync(a => a.Login == login);

            if (account != null && account.Cards.Any())
            {
                foreach (var card in account.Cards)
                {
                    card.SentTransactions = await _context.Transaction
                        .Where(x => x.CardSenderID == card.Id)
                        .ToListAsync();

                    card.ReceivedTransactions = await _context.Transaction
                        .Where(x => x.CardReceiverID == card.Id)
                        .ToListAsync();
                }
            }

            return account;
        }


        public async Task<Account?> GetByLoginAndPassword(string login, string password)
        {
            return await _context.Account.SingleOrDefaultAsync(a => a.Login == login && a.Password == password);
        }

        public async Task<Account> Post(Account account)
        {
            await _context.Account.AddAsync(account);
            await _context.SaveChangesAsync();
            return account;
        }

        public async Task<Account> Update(Guid id, Account accoun)
        {
            var account = await _context.Account.FindAsync(id);
            if (account == null)
            {
                throw new KeyNotFoundException($"Account with ID {id} not found.");
            }

            var validationResult = await _validator.ValidateAsync(account);
            if (!validationResult.IsValid)
            {
                var errorMessages = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errorMessages);
            }
            _context.Account.Update(account);
            await _context.SaveChangesAsync();
            return account;
        }

        public async Task<Account> Delete(Guid id)
        {
            var account = await _context.Account.FindAsync(id);
            if (account == null)
            {
                throw new KeyNotFoundException($"Account with ID {id} not found.");
            }

            account.EmployeeID = Guid.Empty;
            account.CustomerID = Guid.Empty;
            _context.Account.Remove(account);
            await _context.SaveChangesAsync();
            return account;
        }
    }
}


