using DakarRally.Data;
using DakarRally.Data.Models;
using DakarRally.Domain.Enums;
using System;
using System.Linq;

namespace DakarRally.Services.VehicleServices
{
    public class VehicleService : IVehicleService
    {
        private readonly IUnitOfWork unitOfWork;
        public VehicleService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public double GetVehicleDistance(int VehicleId)
        {
            var vehicle = GetVehicle(VehicleId);

            var now = DateTime.UtcNow;

            var timePassed = now.Subtract(vehicle.Race.Start.Value).TotalHours;

            var distance = timePassed * vehicle.Type.Speed;

            if (vehicle.HeavyMalfunction != null && now >= vehicle.HeavyMalfunction.Time)
            {
                return Math.Round(vehicle.HeavyMalfunction.Time.Subtract(vehicle.Race.Start.Value).TotalHours * vehicle.Type.Speed, 2);
            }

            var lightMalfunctionsTime = vehicle.LightMalfunctions.Where(x => x.Time <= now).FirstOrDefault();

            if (lightMalfunctionsTime != null)
            {
                var beforeDefectTime = lightMalfunctionsTime.Time.Subtract(vehicle.Race.Start.Value).TotalHours;
                var afterRepairTime = Math.Max(0, timePassed - beforeDefectTime - vehicle.Type.RepairTime);

                return Math.Round((beforeDefectTime + afterRepairTime) * vehicle.Type.Speed, 2);
            }

            return Math.Round(distance > vehicle.Race.DistanceKm ? vehicle.Race.DistanceKm: distance, 2);

        }

        public VehicleRaceStatusEnum GetVehicleStatus(int VehicleId)
        {
            var vehicle = GetVehicle(VehicleId);
            var now = DateTime.UtcNow;

            var LatestLightMalfunction = vehicle.LightMalfunctions.Where(x => x.Time <= now).LastOrDefault();

            if (vehicle.HeavyMalfunction != null && vehicle.HeavyMalfunction.Time <= now)
            {
                return VehicleRaceStatusEnum.BrokenDown;
            }
            else if(LatestLightMalfunction != null && (now.AddHours(-vehicle.Type.RepairTime)) < LatestLightMalfunction.Time)
            {
                return VehicleRaceStatusEnum.Reparing;
            }
            else if(GetVehicleDistance(VehicleId) >= vehicle.Race.DistanceKm)
            {
                return VehicleRaceStatusEnum.Finished;
            }
            else
            {
                return VehicleRaceStatusEnum.Running;
            }
        }
         
        public double? GetVehicleFinishTime(int VehicleId)
        {
            var vehicle = GetVehicle(VehicleId);

            if(vehicle.HeavyMalfunction == null && GetVehicleDistance(VehicleId) >= vehicle.Race.DistanceKm)
            {
                var finishTime = (vehicle.Race.DistanceKm / (double)vehicle.Type.Speed) + (vehicle.LightMalfunctions.Count * vehicle.Type.RepairTime);
                return Math.Round(finishTime, 2);
            }

            return null;
        }

        private Vehicle GetVehicle(int VehicleId)
        {
            var vehicle = this.unitOfWork.Repository<Vehicle>()
               .FindByInclude(x => x.Id == VehicleId, x => x.Race, x => x.LightMalfunctions, x => x.HeavyMalfunction).FirstOrDefault();
            return vehicle;
        }
    }
}
