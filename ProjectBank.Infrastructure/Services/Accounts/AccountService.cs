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
    public class AccountService(DataContext context) : IAccountService
    {
        public async Task<ActionResult<List<Account>>> Get(string? search, string? sortItem, string? sortOrder)
        {
            IQueryable<Account> accounts = context.Account;

            if (!string.IsNullOrEmpty(search))
            {
                accounts = accounts.Where(n => n.Name.Contains(search, StringComparison.CurrentCultureIgnoreCase));
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

        public async Task<Account> Get(Guid Id)
        {
             var account = await context.Account
                .Include(a => a.Customer)
                .Include(a => a.Cards)
                    .ThenInclude(c => c.SentTransactions)
                .Include(a => a.Cards)
                    .ThenInclude(c => c.ReceivedTransactions)
                .SingleOrDefaultAsync(a => a.Id == Id);

            if (account != null && account.Cards.Any())
            {
                foreach (var card in account.Cards)
                {
                    card.SentTransactions = await context.Transaction
                        .Where(x => x.CardSenderID == card.Id)
                        .ToListAsync();

                    card.ReceivedTransactions = await context.Transaction
                        .Where(x => x.CardReceiverID == card.Id)
                        .ToListAsync();
                }
            }

            return account;
        }


        public async Task<Account?> GetByLoginAsync(string login)
        {
            return await context.Account.SingleOrDefaultAsync(a => a.Login == login);
        }

        public async Task<Account?> GetByIdAcync(Guid Id)
        {
            return await context.Account.SingleOrDefaultAsync(a => a.Id == Id);
        }

        public async Task<Account> Post(Account account)
        {
            await context.Account.AddAsync(account);
            await context.SaveChangesAsync();
            return account;
        }

        public async Task<Account> Update(Guid id, Account accoun)
        {
            var account = await context.Account.FindAsync(id) ?? throw new KeyNotFoundException($"Account with ID {id} not found.");
            context.Account.Update(account);
            await context.SaveChangesAsync();
            return account;
        }

        public async Task<Account> Delete(Guid id)
        {
            var account = await context.Account.FindAsync(id) ?? throw new KeyNotFoundException($"Account with ID {id} not found.");
            account.EmployeeID = Guid.Empty;
            account.CustomerID = Guid.Empty;
            context.Account.Remove(account);
            await context.SaveChangesAsync();
            return account;
        }
    }
}


