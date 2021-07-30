using DakarRally.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DakarRally.Data.EntitiesConfig
{
    public class VehicleConfig : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ManufacturingDate).IsRequired();
            builder.Property(x => x.Model).IsRequired().HasMaxLength(50);
            builder.Property(x => x.TeamName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.VehicleTypeId).IsRequired();
            builder.Property(x => x.RaceId).IsRequired();


            builder.HasOne(x => x.Race)
                .WithMany(x => x.Vehicles)
                .HasForeignKey(x => x.RaceId)
                .HasConstraintName("FK_Vehicle_Race");

            builder.HasOne(x => x.Type)
                .WithMany(x => x.Vehicles)
                .HasForeignKey(x => x.VehicleTypeId)
                .HasConstraintName("FK_Vehicle_VehicleType");
        }
    }
}
