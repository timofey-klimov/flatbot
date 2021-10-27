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


            builder.Metadata
                .FindNavigation(nameof(UserContext.PostedNotifications))
                .SetField("_postedNotifications");

            builder.Metadata
                .FindNavigation(nameof(UserContext.UserRoomCounts))
                .SetField("_roomCounts");
        }
    }
}
