﻿using Microsoft.AspNetCore.Mvc;
using ProjectBank.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.Infrastructure.Services.Accounts
{
    public interface IAccountService
    {
        Task<ActionResult<List<AccountRequestModel>>> Get(string? Search, string? SortItem, string? SortOrder);
        Task<Account> Post(AccountRequestModel account);
        Task<Account> Update(Guid id, AccountRequestModel account);
        Task<Account> Delete(Guid id);
    }
}
