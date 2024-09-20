using FluentValidation;
using MediatR;
using ProjectBank.Application.Features.Customers.Commands;
using ProjectBank.BusinessLogic.Features.Accounts.Commands;
using ProjectBank.Infrastructure.Entities;
using ProjectBank.Infrastructure.Services.Accounts;
using ProjectBank.Infrastructure.Services.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.Features.Accounts.Handlers
{
    internal class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, Account>
    {
        private readonly IAccountService _accountService;
        private readonly IValidator<Account> _validator;

        public CreateAccountCommandHandler(IAccountService accountService, IValidator<Account> validator)
        {
            _accountService = accountService;
            _validator = validator;
        }

        public async Task<Account> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var account = new Account()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                CustomerID = request.CustomerID,
                Login = request.Login,
                Password = request.Password,
            };

            var validationResult = await _validator.ValidateAsync(account, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errorMessages = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errorMessages);
            }

            await _accountService.Post(account);
            return account;

        }
    }
}
