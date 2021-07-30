using DakarRally.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DakarRally.Data.EntitiesConfig
{
    public class VehicleTypeConfig : IEntityTypeConfiguration<VehicleType>
    {
        public void Configure(EntityTypeBuilder<VehicleType> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Type).IsRequired().HasMaxLength(20);
            builder.Property(x => x.HeavyDefect).IsRequired();
            builder.Property(x => x.LightDefect).IsRequired();
            builder.Property(x => x.SubType).HasMaxLength(20);
            builder.Property(x => x.Speed).IsRequired();
            builder.Property(x => x.RepairTime).IsRequired();

        }
     }
}
