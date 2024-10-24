using ProjectBank.BusinessLogic.Features.Authentication.Commands;
using ProjectBank.DataAcces.Entities;

namespace ProjectBank.BusinessLogic.Services
{
    public interface IAuthenticationService
    {
        Task<string> AuthenticateAsync(string login, string password);
        Task<string> RegisterAsync(RegisterCommand request);
    }
}