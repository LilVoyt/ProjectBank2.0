using AutoMapper;
using FluentValidation;
using MediatR;
using ProjectBank.BusinessLogic.Features.Authentication.Commands;
using ProjectBank.BusinessLogic.Security;
using ProjectBank.DataAcces.Entities;
using ProjectBank.DataAcces.Services.Accounts;
using ProjectBank.DataAcces.Services.Customers;


namespace ProjectBank.BusinessLogic.Features.Authentication.Handlers
{
    public class RegisterCommandHandler(IMapper mapper, IValidator<Account> accountValidator,
        IAccountService accountService, ICustomerService customerService,
        IValidator<Customer> customerValidator, IPasswordHasher passwordHasher, CreateJwt jwt)
        : IRequestHandler<RegisterCommand, Account>
    {
        public async Task<Account> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {

            var customer = mapper.Map<Customer>(request);

            var customerValidationResult = await customerValidator.ValidateAsync(customer, cancellationToken);
            if (!customerValidationResult.IsValid)
            {
                var errorMessages = string.Join("; ", customerValidationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errorMessages);
            }
            await customerService.Post(customer);

            var account = mapper.Map<Account>(request);
            account.CustomerID = customer.Id;
            account.Token = jwt.Handle(account);
            account.Password = passwordHasher.Hash(account.Password);

            var accountValidationResult = await accountValidator.ValidateAsync(account, cancellationToken);
            if (!accountValidationResult.IsValid)
            {
                var errorMessages = string.Join("; ", accountValidationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errorMessages);
            }
            await accountService.Post(account);

            return account;
        }
    }
}
