using FluentValidation;
using MediatR;
using ProjectBank.BusinessLogic.Features.Customers.Commands;
using ProjectBank.BusinessLogic.Features.Accounts.Commands;
using ProjectBank.DataAcces.Entities;
using ProjectBank.DataAcces.Services.Accounts;
using ProjectBank.DataAcces.Services.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace ProjectBank.BusinessLogic.Features.Accounts.Handlers
{
    public class CreateAccountCommandHandler(IAccountService accountService, IValidator<Account> validator, IMapper mapper) : IRequestHandler<CreateAccountCommand, Account>
    {
        public async Task<Account> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var account = mapper.Map<Account>(request);

            var validationResult = await validator.ValidateAsync(account, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errorMessages = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errorMessages);
            }

            await accountService.Post(account);
            return account;

        }
    }
}
