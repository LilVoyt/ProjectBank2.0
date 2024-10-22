using FluentValidation;
using ProjectBank.BusinessLogic.Features.Authentication.Commands;
using ProjectBank.BusinessLogic.Features.Authentication.Validator.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.Features.Authentication.Validators
{
    public class RegisterValidator : AbstractValidator<RegisterCommand>
    {
        public readonly IAuthenticationValidationService _authenticationValidationService;
        public RegisterValidator(IAuthenticationValidationService authenticationValidationService)
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
