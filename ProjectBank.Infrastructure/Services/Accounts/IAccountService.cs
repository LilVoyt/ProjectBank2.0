using Microsoft.AspNetCore.Mvc;
using ProjectBank.DataAcces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.DataAcces.Services.Accounts
{
    public interface IAccountService
    {
        Task<ActionResult<List<Account>>> Get(string? Search, string? SortItem, string? SortOrder);
        Account GetByLogin(string login);
        Account? GetByLoginAndPassword(string login, string password);
        Task<Account> Post(Account account);
        Task<Account> Update(Guid id, Account account);
        Task<Account> Delete(Guid id);
    }
}
