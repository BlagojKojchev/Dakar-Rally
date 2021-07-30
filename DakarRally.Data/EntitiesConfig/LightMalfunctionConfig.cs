using DakarRally.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
namespace DakarRally.Data.EntitiesConfig
{
    public class LightMalfunctionConfig : IEntityTypeConfiguration<LightMalfunction>
    {
        public void Configure(EntityTypeBuilder<LightMalfunction> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Time).IsRequired();
            builder.Property(x => x.VehicleId).IsRequired();

            builder.HasOne(x => x.Vehicle)
              .WithMany(x => x.LightMalfunctions)
              .HasForeignKey(x => x.VehicleId)
              .HasConstraintName("FK_LightMalfunction_Vehicle");
        }
    }
}
