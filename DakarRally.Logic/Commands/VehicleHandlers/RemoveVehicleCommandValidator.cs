using DakarRally.Logic.CommonValidation;
using FluentValidation;
namespace DakarRally.Logic.Commands.VehicleHandlers
{
    public class RemoveVehicleCommandValidator : AbstractValidator<RemoveVehicleCommand>
    {
        public RemoveVehicleCommandValidator(IVehicleValidation vehicleValidationCommon)
        {
            this.RuleFor(x => x.VehicleId)
                .NotNull()
                .WithMessage("VehicleId can not be Null")
                .Must(vehicleValidationCommon.CheckVehicle)
                .WithMessage("No such vehicle exists");

        }
    }
}
