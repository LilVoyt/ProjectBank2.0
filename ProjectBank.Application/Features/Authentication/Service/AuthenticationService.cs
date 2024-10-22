using System.Threading.Tasks;
using ProjectBank.BusinessLogic.Security.Jwt;
using ProjectBank.BusinessLogic.Security.Password;
using ProjectBank.DataAcces.Services.Accounts;
using ProjectBank.DataAcces.Entities;
using Microsoft.AspNetCore.Authentication;
using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Identity;
using ProjectBank.DataAcces.Services.Customers;
using ProjectBank.BusinessLogic.Features.Authentication.Commands;

namespace ProjectBank.BusinessLogic.Services
{
    public class AuthenticationService(
        IAccountService accountService,
        ICustomerService customerService,
        IMapper mapper,
        IPasswordHasher passwordHasher,
        IJwtHandler jwtHandler) : IAuthenticationService
    {
        public async Task<Account> AuthenticateAsync(string login, string password)
        {
            var account = await accountService.GetAsync(login) ?? throw new KeyNotFoundException();

            bool isPasswordValid = passwordHasher.Verify(password, account.Password);
            if (!isPasswordValid)
            {
                throw new KeyNotFoundException();
            }
            account.Token = jwtHandler.Handle(account);

            return account;
        }

        public async Task<Account> RegisterAsync(RegisterCommand request)
        {
            var customer = mapper.Map<Customer>(request);

            await customerService.Post(customer);

            var account = mapper.Map<Account>(request, opt =>
            {
                opt.Items["CustomerId"] = customer.Id;
                opt.Items["HashedPassword"] = passwordHasher.Hash(request.Password);
            });
            account.Token = jwtHandler.Handle(account);

            await accountService.Post(account);
            return account;
        }
    }
}
