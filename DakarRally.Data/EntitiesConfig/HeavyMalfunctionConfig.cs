using DakarRally.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DakarRally.Data.EntitiesConfig
{
    public class HeavyMalfunctionConfig : IEntityTypeConfiguration<HeavyMalfunction>
    {
        public void Configure(EntityTypeBuilder<HeavyMalfunction> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Time).IsRequired();
            builder.Property(x => x.VehicleId).IsRequired();

            builder.HasOne(x => x.Vehicle)
              .WithOne(x => x.HeavyMalfunction)
              .HasForeignKey<HeavyMalfunction>(x => x.VehicleId)
              .HasConstraintName("FK_HeavyMalfunction_Vehicle");
        }
    }
}
