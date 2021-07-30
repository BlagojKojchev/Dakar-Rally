using DakarRally.Data.Models;
using DakarRally.Domain.Results;
using System.Collections.Generic;

namespace DakarRally.Services.RaceServices
{
    public interface IRaceStatisticsService
    {
        public IEnumerable<VehicleStatistics> GetStatistics(IEnumerable<Vehicle> vehicles);
    }
}
