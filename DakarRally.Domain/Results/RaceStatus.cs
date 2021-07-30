using System.Collections.Generic;

namespace DakarRally.Domain.Results
{
    public class RaceStatus
    {
        public string status { get; set; }
        public IEnumerable<string> NumberOfVehiclesByStatus { get; set; }
        public IEnumerable<string> NumberOfVehiclesByType { get; set; }
    }
}
