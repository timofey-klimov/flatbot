using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Implemtation.DataAccess.Configuration
{
    public class DistrictConfiguration : IEntityTypeConfiguration<District>
    {
        public void Configure(EntityTypeBuilder<District> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(30);

            builder.HasMany(x => x.UserContexts)
                .WithMany(x => x.Disctricts);

            builder.HasData(
                new District[]
                {
                    new District() { Name = "ЦАО", Id = 1 },
                    new District() { Name = "СВАО", Id = 2 },
                    new District() { Name = "СЗАО", Id = 3 },
                    new District() { Name = "ЮАО", Id = 4 },
                    new District() { Name = "ЗАО", Id = 5 },
                    new District() { Name = "САО", Id = 6 },
                    new District() { Name = "ВАО", Id = 7 },
                    new District() { Name = "ЮВАО", Id = 8 },
                    new District() { Name = "ЮЗАО", Id = 9 }
                });
        }
    }
}
