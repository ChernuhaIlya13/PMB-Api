using FluentValidation;
using PMB.Models.V1.Requests;

namespace PMB.WebApi.Validators
{
    public class UnFreezeKeyRequestValidator: AbstractValidator<FreezeKeyRequest>
    {
        public UnFreezeKeyRequestValidator()
        {
            RuleFor(x => x.Login)
                .NotEmpty();

            RuleFor(x => x.Key)
                .NotEmpty();
        }
    }
}