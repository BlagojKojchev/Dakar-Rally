using DakarRally.Logic.CommonValidation;
using FluentValidation;

namespace DakarRally.Logic.Commands.VehicleHandlers
{
    public class AddVehicleToRaceCommandValidator : AbstractValidator<AddVehicleToRaceCommand>
    {
        public AddVehicleToRaceCommandValidator(IVehicleTypeValidation vehicleTypeValidation, IRaceValidation raceValidation)
        {
            this.RuleFor(x => x.Vehicle)
                .Must(vehicleTypeValidation.CheckVehicleType)
                .WithMessage("Can not add vehicle of such type");

            this.RuleFor(x => x.Vehicle.RaceYear)
                .NotNull()
                .WithMessage("Race Year can not be Null")
                .NotEmpty()
                .WithMessage("Race Year can not be Empty")
                .Must(raceValidation.RaceExist)
                .WithMessage("No such race exists");

            this.RuleFor(x => x.Vehicle.Type)
                .NotNull()
                .WithMessage("Type can not be Null")
                .NotEmpty()
                .WithMessage("Type can not be Empty");

            this.RuleFor(x => x.Vehicle.ManufacturingDate)
                .NotNull()
                .WithMessage("Manufacturing Date can not be Null")
                .NotEmpty()
                .WithMessage("Manufacturing Date can not be Empty");

            this.RuleFor(x => x.Vehicle.TeamName)
                .NotNull()
                .WithMessage("Team Name can not be Null")
                .NotEmpty()
                .WithMessage("Team Name can not be Empty");
        }
    }
}
