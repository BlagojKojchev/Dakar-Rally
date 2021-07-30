using DakarRally.Domain.Enums;

namespace DakarRally.Services.VehicleServices
{
    public interface IVehicleService
    {
        double GetVehicleDistance(int VehicleId);
        double? GetVehicleFinishTime(int VehicleId);
        VehicleRaceStatusEnum GetVehicleStatus(int VehicleId);
    }
}
