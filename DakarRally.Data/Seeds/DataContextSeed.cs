using DakarRally.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DakarRally.Data.Seeds
{
    public static class DataContextSeed
    {
        public static void Seed(this ModelBuilder modeBuilder)
        {
            CreateVehicleTypeData(modeBuilder);
        }

        public static void CreateVehicleTypeData(ModelBuilder modelBuilder)
        {
            var vehicleType = VehicleTypeData.VehicleTypeDataList();
            modelBuilder.Entity<VehicleType>().HasData(vehicleType);
        }
    }
}
