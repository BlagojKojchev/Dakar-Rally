using DakarRally.Domain.Dtos;

namespace DakarRally.Domain.Results
{
    public class VehicleRaceStatus
    {
        public VehicleDto Vehicle { get; set; }
        public int Place { get; set; }
        public double Distance { get; set; }
    }
}
