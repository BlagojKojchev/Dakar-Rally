using DakarRally.Domain.Dtos;
using System.Collections.Generic;

namespace DakarRally.Domain.Results
{
    public class VehicleStatistics : VehicleRaceStatus
    {
        public HeavyMalfunctionDto HeavyMalfunction { get; set; }
        public IEnumerable<LightMalfunctionDto> LightMalfunction { get; set; }
        public string Status { get; set; }
        public double FinishTime { get; set; }
    }
}
