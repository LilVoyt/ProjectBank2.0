using AutoMapper;
using FluentValidation;
using ProjectBank.BusinessLogic.Features.Accounts.Queries;
using ProjectBank.BusinessLogic.Models;
using ProjectBank.DataAcces.Entities;
using ProjectBank.DataAcces.Services.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.Features.Accounts.Service
{
    public class AccountLogicService(IAccountService accountService, IMapper mapper) : IAccountLogicService
    {
        public async Task<AccountDto> GetDto(GetByIdQuery request)
        {
            var account = await accountService.Get(request.Id) ?? throw new KeyNotFoundException("Account not found.");
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
