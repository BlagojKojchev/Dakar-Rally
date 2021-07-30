using DakarRally.Logic.CommonValidation;
using FluentValidation;

namespace DakarRally.Logic.Queries.RaceQueries
{
    public class GetRaceStatusQueryValidator : AbstractValidator<GetRaceStatusQuery>
    {
        public GetRaceStatusQueryValidator(IRaceValidation raceValidation)
        {
            this.RuleFor(x => x.RaceYear)
                .NotEmpty()
                .WithMessage("RaceYear can not be null")
                .NotEmpty()
                .WithMessage("RaceYear can not be empty")
                .Must(raceValidation.RaceExist)
                .WithMessage("Race does not exist");
        }
    }
}
