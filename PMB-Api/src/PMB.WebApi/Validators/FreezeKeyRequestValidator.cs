using FluentValidation;
using PMB.Models.V1.Requests;

namespace PMB.WebApi.Validators
{
    public class FreezeKeyRequestValidator: AbstractValidator<FreezeKeyRequest>
    {
        public FreezeKeyRequestValidator()
        {
            RuleFor(x => x.Login)
                .NotEmpty();

            RuleFor(x => x.Key)
                .NotEmpty();
        }
    }
}