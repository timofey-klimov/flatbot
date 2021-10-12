using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Implemtation.DataAccess.Configuration
{
    public class TimedJobManagerConfiguration : IEntityTypeConfiguration<TimedJobManager>
    {
        public void Configure(EntityTypeBuilder<TimedJobManager> builder)
        {
            builder.Property(x => x.SheduleRunTime)
                .HasColumnType("datetime2(0)");
        }
    }
}
