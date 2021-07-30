using DakarRally.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DakarRally.Data.DataContext
{
    public interface IDakarRallyContext
    {
        DbSet<Vehicle> Vheicle { get; set; }
        DbSet<Race> Race { get; set; }
        DbSet<VehicleType> VehicleType { get; set; }
        DbSet<HeavyMalfunction> HeavyMalfunction { get; set; }
        DbSet<LightMalfunction> LightMalfunction { get; set; }

        Task<int> SaveChangesAsync();
    }
}
