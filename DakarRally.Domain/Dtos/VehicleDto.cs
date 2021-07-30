using System;

namespace DakarRally.Domain.Dtos
{
    public class VehicleDto
    {
        public string TeamName { get; set; }
        public string Model { get; set; }
        public DateTime ManufacturingDate { get; set; }
        public string Type { get; set; }
        public string SubType { get; set; }
        public int RaceYear { get; set; }
    }
}
