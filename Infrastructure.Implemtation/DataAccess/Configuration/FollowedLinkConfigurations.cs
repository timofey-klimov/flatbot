using Entities.Models.UserAgregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Implemtation.DataAccess.Configuration
{
    public class FollowedLinkConfigurations : IEntityTypeConfiguration<FollowedLink>
    {
        public void Configure(EntityTypeBuilder<FollowedLink> builder)
        {
            builder.HasKey(x => x.Id);

            builder.ToTable("FollowedLinks");
        }
    }
}
