using DakarRally.Domain;
using System;

namespace DakarRally.Logic.CommonValidation
{
    public interface IVehicleValidation
    {
        bool CheckVehicle(int vehicleId);   
        bool CheckVehicleManufacturingDate(DateTime? manufacturingDate);
        bool CheckVehicleModel(string Model);
        bool CheckVehicleTeam(string Team);

    }
}
