using FluentValidation;
using PMB.Models.PositiveModels;
using PMB.Models.V1.Requests;

namespace PMB.WebApi.Validators
{
    public class GetForksByQueryRequestValidator: AbstractValidator<GetForksByQueryRequest>
    {
        public GetForksByQueryRequestValidator()
        {
            RuleFor(x => x)
                .Must(x => x.Bookmakers is not null ||
                           x.Sports is not null ||
                           x.BetTypes is not null ||
                           x.CridIds is not null)
                .WithMessage("One of the parameters should be given");
            
            RuleForEach(x => x.Bookmakers).NotEmpty().When(x => x.Bookmakers is not null);
            RuleForEach(x => x.Bookmakers).IsEnumName(typeof(Bookmaker)).When(x => x.Bookmakers is not null);
            
            RuleForEach(x => x.Sports).NotEmpty().When(x => x.Sports is not null);
            RuleForEach(x => x.Sports).IsEnumName(typeof(Sport)).When(x => x.Sports is not null);
            
            RuleForEach(x => x.BetTypes).NotEmpty().When(x => x.BetTypes is not null);
            RuleForEach(x => x.BetTypes).IsEnumName(typeof(BetType)).When(x => x.BetTypes is not null);
            
            RuleForEach(x => x.CridIds).NotEmpty().When(x => x.CridIds is not null);
        }
    }
}