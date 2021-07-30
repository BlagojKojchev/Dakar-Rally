using DakarRally.Data.Seeds;
using DakarRally.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Threading.Tasks;

namespace DakarRally.Data.DataContext
{
    public class DakarRallyContext : DbContext, IDakarRallyContext
    {
        public DakarRallyContext(DbContextOptions<DakarRallyContext> options) : base(options)
        {
            OpenConnection();
        }

        public virtual DbSet<Vehicle> Vheicle { get; set; }
        public virtual DbSet<Race> Race { get; set; }
        public virtual DbSet<VehicleType> VehicleType { get; set; }
        public virtual DbSet<HeavyMalfunction> HeavyMalfunction { get; set; }
        public virtual DbSet<LightMalfunction> LightMalfunction { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseSqlite("FileName=DakarRally", option => {
                option.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            base.OnConfiguring(dbContextOptionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
        }

        private void OpenConnection()
        {
            this.Database.OpenConnection();
            this.Database.EnsureCreated();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
