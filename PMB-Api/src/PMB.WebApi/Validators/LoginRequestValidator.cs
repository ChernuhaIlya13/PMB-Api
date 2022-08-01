using FluentValidation;
using PMB.Models.V1.Requests;

namespace PMB.WebApi.Validators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Login)
                .NotEmpty();

            RuleFor(x => x.Password)
                .NotEmpty();
        }
    }
}