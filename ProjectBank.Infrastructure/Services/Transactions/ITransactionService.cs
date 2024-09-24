using Microsoft.AspNetCore.Mvc;
using ProjectBank.DataAcces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.DataAcces.Services.Transactions
{
    public interface ITransactionService
    {
        Task<List<Transaction>> Get(Guid? sender, Guid? receiver, string? sortItem, string? sortOrder);
        Task<Transaction> Post(Transaction transaction);
        Task<Transaction> Update(Guid id, Transaction transaction);
        Task<Transaction> Delete(Guid id);
    }
}
