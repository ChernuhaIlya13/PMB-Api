using FluentValidation;
using PMB.Models.V1.Requests;

namespace PMB.WebApi.Validators
{
    public class RegisterRequestValidator: AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty();

            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Invalid email address");
        }
    }
}