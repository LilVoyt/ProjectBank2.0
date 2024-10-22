using ProjectBank.BusinessLogic.Features.Authentication.Commands;
using ProjectBank.DataAcces.Entities;

namespace ProjectBank.BusinessLogic.Services
{
    public interface IAuthenticationService
    {
        Task<Account> AuthenticateAsync(string login, string password);
        Task<Account> RegisterAsync(RegisterCommand request);
    }
}