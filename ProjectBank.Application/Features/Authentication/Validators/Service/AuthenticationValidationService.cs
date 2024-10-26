using System.Text.RegularExpressions;


namespace ProjectBank.BusinessLogic.Features.Authentication.Validator.Login
{
    public class AuthenticationValidationService : IAuthenticationValidationService
    {
        public bool IsValidLogin(string login)
        {
            var loginRegex = @"^(?=.{3,20}$)[a-zA-Z0-9!@#$%^&*()_+-=]*$";
            return Regex.IsMatch(login, loginRegex);
        }

        public bool IsValidPassword(string password)
        {
            var passwordRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,30}$";
            return Regex.IsMatch(password, passwordRegex);
        }

    }
}