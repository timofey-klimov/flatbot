using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Implemtation.DataAccess.Configuration
{
    public class DisctrictConfiguration : IEntityTypeConfiguration<Disctrict>
    {
        public void Configure(EntityTypeBuilder<Disctrict> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(30);

            builder.HasMany(x => x.UserContexts)
                .WithMany(x => x.Disctricts);

            builder.HasData(
                new Disctrict[]
                {
                    new Disctrict() { Name = "ЦАО", Id = 1},
                    new Disctrict() { Name = "СВАО", Id = 2 },
                    new Disctrict() { Name = "СЗАО", Id = 3 },
                    new Disctrict() { Name = "ЮАО", Id = 4 },
                    new Disctrict() { Name = "ЗАО", Id = 5 },
                    new Disctrict() { Name = "ЮВАО", Id = 6 },
                    new Disctrict() { Name = "ЮЗАО", Id = 7 }
                });
        }
    }
}
