using System;
using System.Collections.Generic;

namespace DakarRally.Data.Models
{
    public class Race
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? Finish { get; set; }
        public int DistanceKm { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
