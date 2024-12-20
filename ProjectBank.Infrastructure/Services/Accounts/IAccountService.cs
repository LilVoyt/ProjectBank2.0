﻿using Microsoft.AspNetCore.Mvc;
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
        Task<ActionResult<List<Account>>> Get(string? search, string? sortItem, string? sortOrder);
        Task<Account> Get(Guid Id);
        Task<Account?> GetByLoginAsync(string login);
        Task<Account> Post(Account account);
        Task<Account> Update(Guid id, Account account);
        Task<Account> Delete(Guid id);
    }
}
