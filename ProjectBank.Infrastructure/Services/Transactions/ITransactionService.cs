using Microsoft.AspNetCore.Mvc;
using ProjectBank.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.Infrastructure.Services.Transactions
{
    public interface ITransactionService
    {
        Task<ActionResult<List<TransactionRequestModel>>> Get(Guid? search, string? sortItem, string? sortOrder);
        Task<Transaction> Post(TransactionRequestModel transaction);
        Task<Transaction> Update(Guid id, TransactionRequestModel transaction);
        Task<Transaction> Delete(Guid id);
    }
}
