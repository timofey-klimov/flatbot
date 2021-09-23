using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Implemtation.DataAccess.Configuration
{
    public class ProxyConfiguration : IEntityTypeConfiguration<Proxy>
    {
        public void Configure(EntityTypeBuilder<Proxy> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Ip)
                .HasMaxLength(50);

            builder.Property(x => x.Port)
                .HasColumnType("int");
        }
    }
}
