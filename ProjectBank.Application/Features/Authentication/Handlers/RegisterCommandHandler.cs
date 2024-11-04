using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ProjectBank.BusinessLogic.Features.Authentication.Commands;
using ProjectBank.BusinessLogic.Security.Jwt;
using ProjectBank.BusinessLogic.Security.Password;
using ProjectBank.DataAcces.Entities;
using ProjectBank.DataAcces.Services.Accounts;
using ProjectBank.DataAcces.Services.Customers;


namespace ProjectBank.BusinessLogic.Features.Authentication.Handlers
{
    public class RegisterCommandHandler(ICustomerService customerService, IMapper mapper, IPasswordHasher passwordHasher, IJwtHandler jwtHandler, IAccountService accountService)
        : IRequestHandler<RegisterCommand, string>
    {
        public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var customer = mapper.Map<Customer>(request);

            await customerService.Post(customer);

            var account = mapper.Map<Account>(request, opt =>
            {
                opt.Items["CustomerId"] = customer.Id;
                opt.Items["HashedPassword"] = passwordHasher.Hash(request.Password);
            });
            string Jwt = jwtHandler.Handle(account);

            await accountService.Post(account);
            return Jwt;
        }
    }
}
