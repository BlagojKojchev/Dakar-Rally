using System;

namespace DakarRally.Data.Models
{
    public class LightMalfunction
    {
        public int Id { get; set; }
        public DateTime Time  { get; set;}

        public int VehicleId { get; set; }
        public virtual Vehicle Vehicle { get; set; }
    }
}
