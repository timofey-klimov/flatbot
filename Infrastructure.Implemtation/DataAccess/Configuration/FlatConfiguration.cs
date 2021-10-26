using Entities.Models.FlatEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Implemtation.DataAccess.Configuration
{
    public class FlatConfiguration : IEntityTypeConfiguration<Flat>
    {
        public void Configure(EntityTypeBuilder<Flat> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Metadata
                .FindNavigation(nameof(Flat.RailwayInfos))
                .SetField("_railwayInfos");

            builder
               .Metadata
               .FindNavigation(nameof(Flat.UndergroundInfos))
               .SetField("_undergroundInfos");


            builder
                .Metadata
                .FindNavigation(nameof(Flat.PhotoInfos))
                .SetField("_photoInfos");

            builder
                .Metadata
                .FindNavigation(nameof(Flat.Phones))
                .SetField("_phones");

            builder.Property(x => x.CreateDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("GETDATE()");

            builder.ToTable("Flats");
        }
    }
}
