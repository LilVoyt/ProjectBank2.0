using AutoMapper;
using FluentValidation;
using ProjectBank.BusinessLogic.Features.Accounts.Queries;
using ProjectBank.BusinessLogic.Models;
using ProjectBank.DataAcces.Entities;
using ProjectBank.DataAcces.Services.Accounts;
using ProjectBank.DataAcces.Services.Currencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.Features.Accounts.Service
{
    public class AccountLogicService(IAccountService accountService, IMapper mapper, ICurrencyService currencyService) : IAccountLogicService
    {
        public async Task<AccountDto> GetDto(GetByIdQuery request)
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
                foreach(var card in account.Cards)
                {
                    accountDto.Cards.Add(new CardDto()
                    {
                        Id = card.Id,
                        NumberCard = card.NumberCard,
                        CardName = card.CardName,
                        Pincode = card.Pincode,
                        ExpirationDate = card.ExpirationDate,
                        CVV = card.CVV,
                        Balance = card.Balance,
                        CurrencyCode = currencyService.GetById(card.CurrencyID).Result.CurrencyCode
                    });
                }
               
            }

            return accountDto;
        }
    }
}
