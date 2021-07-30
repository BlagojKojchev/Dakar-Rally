using AutoMapper;
using DakarRally.Data.Models;
using DakarRally.Domain.Dtos;
using DakarRally.Domain.Results;
using DakarRally.Services.VehicleServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DakarRally.Services.RaceServices
{
    public class RaceStatisticsService : IRaceStatisticsService
    {
        private readonly IVehicleService vehicleService;
        private readonly IMapper mapper;
        public RaceStatisticsService(IVehicleService vehicleService, IMapper mapper)
        {
            this.vehicleService = vehicleService;
            this.mapper = mapper;
        }
        public IEnumerable<VehicleStatistics> GetStatistics(IEnumerable<Vehicle> vehicles)
        {
            var vehiclePlaces = new List<VehicleStatistics>();

           

            foreach (var vehicle in vehicles)
            {
                var now = DateTime.UtcNow;
                var lightMalfunctions = vehicle.LightMalfunctions?.Where(x => x.Time <= now);
                var heavyMalfunction = vehicle.HeavyMalfunction?.Time <= now ? vehicle.HeavyMalfunction : null;

                var vehicleDto = mapper.Map<VehicleDto>(vehicle);

                var vehiclePlace = new VehicleStatistics
                {
                    Distance = vehicleService.GetVehicleDistance(vehicle.Id),
                    Vehicle = vehicleDto,
                    FinishTime = vehicleService.GetVehicleFinishTime(vehicle.Id) ?? double.NaN,
                    Status = vehicleService.GetVehicleStatus(vehicle.Id).ToString(),
                    HeavyMalfunction = mapper.Map<HeavyMalfunctionDto>(heavyMalfunction),
                    LightMalfunction = mapper.Map<IEnumerable<LightMalfunctionDto>>(lightMalfunctions)
                };
                vehiclePlaces.Add(vehiclePlace);
            }
            var vehiclePlacesOrdered = vehiclePlaces.OrderByDescending(x => x.Distance).ToList();

            for(int i = 1; i <= vehiclePlacesOrdered.Count-1; i++)
            {
                vehiclePlacesOrdered[i].Place = i;
            }

            return vehiclePlaces.OrderByDescending(x => x.Distance).ToList();
        }
    }
}
