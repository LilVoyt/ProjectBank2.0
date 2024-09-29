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
    public class GetAccountByLoginQueryHandler : IRequestHandler<GetAccountByLoginQuery, AccountDto>
    {
        private readonly IAccountService _accountService;
        private readonly IValidator<Account> _validator;
        private readonly IMapper _mapper;

        public GetAccountByLoginQueryHandler(IAccountService accountService, IValidator<Account> validator, IMapper mapper)
        {
            _accountService = accountService;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<AccountDto> Handle(GetAccountByLoginQuery request, CancellationToken cancellationToken)
        {
            var account = _accountService.GetByLogin(request.Login);

            if (account == null)
            {
                throw new KeyNotFoundException("Account not found.");
            }

            var accountDto = _mapper.Map<AccountDto>(account);

            if (account.Customer != null)
            {
                accountDto.Customer = _mapper.Map<CustomerDto>(account.Customer);
            }
            if (account.Cards != null && account.Cards.Any())
            {
                accountDto.Cards = _mapper.Map<List<CardDto>>(account.Cards);
            }

            return accountDto;
        }

    }
}
