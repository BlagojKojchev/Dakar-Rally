using DakarRally.Logic.CommonValidation;
using FluentValidation;

namespace DakarRally.Logic.Commands.RaceHandlers
{
    public class CreateRaceCommandValidator : AbstractValidator<CreateRaceCommand>
    {
        public CreateRaceCommandValidator(IRaceValidation raceValidation)
        {
            this.RuleFor(x => x.Year)
                .NotNull()
                .WithMessage("Year Year can not be Null")
                .Must(raceValidation.RaceNotExist)
                .WithMessage("Race exists");
            

            this.RuleFor(x => x.DistanceInKm)
               .NotNull()
               .WithMessage("Distance In Km can not be Null");
        }
    }
}
