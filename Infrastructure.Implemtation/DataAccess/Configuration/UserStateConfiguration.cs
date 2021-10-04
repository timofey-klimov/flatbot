using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Implemtation.DataAccess.Configuration
{
    public class UserStateConfiguration : IEntityTypeConfiguration<UserState>
    {
        public void Configure(EntityTypeBuilder<UserState> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.UserContext)
                .WithOne(x => x.State)
                .HasForeignKey<UserState>(x => x.UserContextId);
        }
    }
}
