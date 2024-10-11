using AutoMapper;
using FluentValidation;
using MediatR;
using ProjectBank.BusinessLogic.Features.Accounts.Queries;
using ProjectBank.BusinessLogic.Models;
using ProjectBank.DataAcces.Entities;
using ProjectBank.DataAcces.Services.Accounts;
using ProjectBank.DataAcces.Services.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.Features.Accounts.Handlers
{
    public class GetAccountByLoginQueryHandler(IAccountService accountService, IValidator<Account> validator, IMapper mapper) 
        : IRequestHandler<GetAccountByLoginQuery, AccountDto>
    {
        public async Task<AccountDto> Handle(GetAccountByLoginQuery request, CancellationToken cancellationToken)
        {
            var account = await accountService.GetByLogin(request.Login) ?? throw new KeyNotFoundException("Account not found.");
            var accountDto = mapper.Map<AccountDto>(account);

            if (account.Customer != null)
            {
                accountDto.Customer = mapper.Map<CustomerDto>(account.Customer);
            }
            if (account.Cards != null && account.Cards.Any())
            {
                accountDto.Cards = mapper.Map<List<CardDto>>(account.Cards);
            }

            return accountDto;
        }

    }
}
