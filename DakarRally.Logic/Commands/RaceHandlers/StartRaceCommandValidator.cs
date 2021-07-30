using DakarRally.Logic.CommonValidation;
using FluentValidation;

namespace DakarRally.Logic.Commands.RaceHandlers
{
    public class StartRaceCommandValidator : AbstractValidator<StartRaceCommand>
    {
        public StartRaceCommandValidator(IRaceValidation raceValidation)
        {
            this.RuleFor(x => x.Year)
                .NotNull()
                .WithMessage("Year Year can not be Null")
                .Must(raceValidation.RaceExist)
                .WithMessage("No such race exists");
        }
    }
}
