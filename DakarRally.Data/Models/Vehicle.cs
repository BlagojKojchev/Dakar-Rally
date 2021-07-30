using System;
using System.Collections.Generic;

namespace DakarRally.Data.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string TeamName { get; set; }
        public string Model { get; set; }
        public DateTime ManufacturingDate { get; set; }

        public int RaceId { get; set; }
        public int VehicleTypeId { get; set; }
        public virtual Race Race { get; set; }
        public virtual VehicleType Type { get; set; }
        public virtual HeavyMalfunction HeavyMalfunction { get; set; }
        public virtual ICollection<LightMalfunction> LightMalfunctions { get; set; }
    }
}