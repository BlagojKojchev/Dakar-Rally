using DakarRally.Logic.CommonValidation;
using FluentValidation;

namespace DakarRally.Logic.Queries.LiderboardHandlers
{
    public class GetLeaderboardForCarTypeQueryValidator :  AbstractValidator<GetLeaderboardForCarTypeQuery>
    {
        public GetLeaderboardForCarTypeQueryValidator(IVehicleTypeValidation vehicleTypeValidation)
        {
            this.RuleFor(x => x.Type)
               .NotNull()
               .WithMessage("Type can not be null")
               .NotEmpty()
               .WithMessage("Type can not be empty")
               .Must(vehicleTypeValidation.CheckVehicleType)
               .WithMessage("No such type exists");
        }


    }
}
