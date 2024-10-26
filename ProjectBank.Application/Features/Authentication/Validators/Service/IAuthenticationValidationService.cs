namespace ProjectBank.BusinessLogic.Features.Authentication.Validator.Login
{
    public interface IAuthenticationValidationService
    {
        bool IsValidLogin(string login);
        bool IsValidPassword(string password);
    }
}