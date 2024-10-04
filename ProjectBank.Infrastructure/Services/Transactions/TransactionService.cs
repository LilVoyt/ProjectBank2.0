using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectBank.DataAcces.Data;
using ProjectBank.DataAcces.Entities;
using ProjectBank.DataAcces.Services.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.Infrastructure.Services.Transactions
{
    public class TransactionService : ITransactionService
    {
        private readonly DataContext _context;

        public TransactionService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Transaction>> Get(Guid? sender, Guid? receiver, string? sortItem, string? sortOrder)
        {
            IQueryable<Transaction> transactions = _context.Transaction;

            if (sender.HasValue)
            {
                transactions = transactions.Where(t => t.CardSenderID == sender);
            }

            if (receiver.HasValue)
            {
                transactions = transactions.Where(t => t.CardReceiverID == receiver);
            }

            Expression<Func<Transaction, object>> selectorKey = sortItem?.ToLower() switch
            {
                "date" => transactions => transactions.Date,
                "sum" => transactions => transactions.Sum,
                _ => transactions => transactions.Date
            };

            transactions = sortOrder?.ToLower() == "desc"
                ? transactions.OrderByDescending(selectorKey)
                : transactions.OrderBy(selectorKey);
            List<Transaction> transactionsList = await transactions.ToListAsync();

            return transactionsList;
        }

        public async Task<Transaction> Post(Transaction transaction)
        {
            await _context.Transaction.AddAsync(transaction);
            await _context.SaveChangesAsync();

            return transaction;
        }

        public async Task<Transaction> Update(Guid id, Transaction requestModel)
        {
            //var transaction = await _context.Transaction.FindAsync(id);
            //if (transaction == null)
            //{
            //    throw new KeyNotFoundException($"Account with ID {id} not found.");
            //}
            //transaction = _mapper.PutRequestModelInTransaction(transaction, requestModel);
            //var validationResult = await _validator.ValidateAsync(transaction);
            //if (!validationResult.IsValid)
            //{
            //    var errorMessages = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
            //    throw new ValidationException(errorMessages);
            //}
            //_context.Transaction.Update(transaction);
            //await _context.SaveChangesAsync();

            return requestModel;
        }

        public async Task<Transaction> Delete(Guid id)
        {
            var transaction = await _context.Transaction.FindAsync(id);
            if (transaction == null)
            {
                throw new KeyNotFoundException($"Account with ID {id} not found.");
            }

            transaction.CardSenderID = Guid.Empty;
            transaction.CardReceiverID = Guid.Empty;
            _context.Transaction.Remove(transaction);
            await _context.SaveChangesAsync();

            return transaction;
        }
    }
}
