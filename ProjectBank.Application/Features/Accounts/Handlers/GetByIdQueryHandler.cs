using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectBank.BusinessLogic.Features.Accounts.Queries;
using ProjectBank.BusinessLogic.Models;
using ProjectBank.DataAcces.Entities;
using ProjectBank.DataAcces.Services.Accounts;
using ProjectBank.DataAcces.Services.Currencies;
using ProjectBank.DataAcces.Services.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.Features.Accounts.Handlers
{
    public class GetByIdQueryHandler(ICurrencyService currencyService, IAccountService accountService, IMapper mapper) 
        : IRequestHandler<GetByIdQuery, AccountDto>
    {
        public async Task<AccountDto> Handle([FromBody]GetByIdQuery request, CancellationToken cancellationToken)
        {
            var account = await accountService.Get(request.Id) ?? throw new KeyNotFoundException("Account not found.");
            var accountDto = new AccountDto()
            {
                Id = account.Id,
                Name = account.Name,
                Role = account.Role,
                Cards = new List<CardDto>(),
                Customer = new CustomerDto()
            };

            if (account.Customer != null)
            {
                accountDto.Customer = mapper.Map<CustomerDto>(account.Customer);
            }
            if (account.Cards != null && account.Cards.Any())
            {
                accountDto.Cards = new List<CardDto>();
                foreach (var card in account.Cards)
                {
                    var currency = await currencyService.GetByIdAsync(card.CurrencyID) ?? throw new KeyNotFoundException();

                    accountDto.Cards.Add(mapper.Map<CardDto>(card, opt =>
                    {
                        opt.Items["CurrencyCode"] = currency.CurrencyCode;
                    }));
                }
            }

            return accountDto;
        }

    }
}
