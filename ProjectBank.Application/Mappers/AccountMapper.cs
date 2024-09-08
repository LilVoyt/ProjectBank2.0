using ProjectBank.Application.Models;
using ProjectBank.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.Application.Mappers
{
    internal class AccountMapper
    {
        public Account GetAccount(AccountRequestModel requestModel)
        {
            return new Account
            {
                Id = Guid.NewGuid(),
                Name = requestModel.Name,
                EmployeeID = requestModel.EmployeeID,
                CustomerID = requestModel.CustomerID
            };
        }

        public AccountRequestModel GetRequestModel(Account account)
        {
            return new AccountRequestModel
            {
                Name = account.Name,
                EmployeeID = account.EmployeeID,
                CustomerID = account.CustomerID
            };
        }

        public Account PutRequestModelInAccount(Account account, AccountRequestModel requestModel)
        {
            account.Name = requestModel.Name;
            account.EmployeeID = requestModel.EmployeeID;
            account.CustomerID = requestModel.CustomerID;

            return account;
        }

        public List<AccountRequestModel> GetRequestModels(List<Account> accounts)
        {
            return accounts.Select(account => GetRequestModel(account)).ToList();
        }
    }
}
