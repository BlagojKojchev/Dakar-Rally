using DakarRally.Logic.CommonValidation;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace DakarRally.Logic.Queries.VehicleHandlers
{
    public class GetVehicleStatisticsQueryValidator : AbstractValidator<GetVehicleStatisticsQuery>
    {
        public GetVehicleStatisticsQueryValidator(IVehicleValidation vehicleValidationCommon)
        {
            this.RuleFor(x => x.VehicleId)
              .NotNull()
              .WithMessage("Type can not be null")
              .NotEmpty()
              .WithMessage("Type can not be empty")
              .Must(vehicleValidationCommon.CheckVehicle)
              .WithMessage("No such vehicle exists");
        }
    }
}
