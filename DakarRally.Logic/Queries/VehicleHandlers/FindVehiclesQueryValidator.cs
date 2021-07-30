using DakarRally.Logic.CommonValidation;
using FluentValidation;

namespace DakarRally.Logic.Queries.VehicleHandlers
{
    public class FindVehiclesQueryValidator : AbstractValidator<FindVehiclesQuery>
    {
       public FindVehiclesQueryValidator(IVehicleValidation vehicleValidationCommon)
        {
            this.RuleFor(x => x.Team)
                .Must(vehicleValidationCommon.CheckVehicleTeam)
                .WithMessage("No such team exists");

            this.RuleFor(x => x.Model)
               .Must(vehicleValidationCommon.CheckVehicleModel)
               .WithMessage("No such model exists");

            this.RuleFor(x => x.manufacturingDate)
               .Must(vehicleValidationCommon.CheckVehicleManufacturingDate)
               .WithMessage("No such manufacturingDate exists");
        }
    }
}
