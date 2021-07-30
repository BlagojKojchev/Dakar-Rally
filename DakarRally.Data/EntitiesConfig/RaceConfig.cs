using DakarRally.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DakarRally.Data.EntitiesConfig
{
    public class RaceConfig : IEntityTypeConfiguration<Race>
    {
        public void Configure(EntityTypeBuilder<Race> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(t => t.Year).IsRequired();
            builder.Property(t => t.Start);
            builder.Property(t => t.Finish);
            builder.Property(t => t.DistanceKm).IsRequired();
        }
    }
}
