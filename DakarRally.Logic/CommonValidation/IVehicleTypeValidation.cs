using DakarRally.Domain.Dtos;

namespace DakarRally.Logic.CommonValidation
{
    public interface IVehicleTypeValidation
    {
        bool CheckVehicleType(VehicleDto vehicle);
        bool CheckVehicleType(string Type);
    }
}
