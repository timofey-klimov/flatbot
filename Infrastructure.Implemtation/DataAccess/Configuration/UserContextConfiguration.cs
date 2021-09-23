using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Implemtation.DataAccess.Configuration
{
    public class UserContextConfiguration : IEntityTypeConfiguration<UserContext>
    {
        public void Configure(EntityTypeBuilder<UserContext> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.MinimumPrice)
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.MaximumPrice)
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.PostedNotifications)
                .HasColumnType("nvarchar(max)");

            builder.Ignore(x => x.NotificationsList);

            builder.HasMany(x => x.Disctricts)
                .WithMany(x => x.UserContexts);
        }
    }
}
