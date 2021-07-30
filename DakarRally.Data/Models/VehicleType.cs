using System.Collections.Generic;

namespace DakarRally.Data.Models
{
    public class VehicleType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string SubType { get; set; }
        public int Speed { get; set; }
        public int LightDefect { get; set; }
        public int HeavyDefect { get; set; }
        public int RepairTime { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}