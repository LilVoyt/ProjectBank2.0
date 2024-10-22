using FluentValidation;
using ProjectBank.BusinessLogic.Features.Authentication.Commands;
using ProjectBank.BusinessLogic.Features.Authentication.Validator.Login;

namespace ProjectBank.BusinessLogic.Features.Authentication.Validators
{
    public class LoginValidator : AbstractValidator<LoginCommand>
    {
        private readonly IAuthenticationValidationService _authenticationValidationService;

        public LoginValidator(IAuthenticationValidationService authenticationValidationService)
        {
            _authenticationValidationService = authenticationValidationService;

            RuleFor(x => x.Login)
                .Must(_authenticationValidationService.IsValidLogin)
                .WithMessage("Invalid login format.");

            RuleFor(x => x.Password)
                .Must(_authenticationValidationService.IsValidPassword)
                .WithMessage("Password must contain at least 8 characters, including uppercase, lowercase, numbers, and special symbols.");
        }
    }
}
