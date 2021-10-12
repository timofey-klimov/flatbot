using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Implemtation.DataAccess.Configuration
{
    public class SheduleJobManagerConfiguration : IEntityTypeConfiguration<SheduleJobManager>
    {
        public void Configure(EntityTypeBuilder<SheduleJobManager> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.PlanningRunTime)
                .HasColumnType("datetime2(0)");
            builder.Property(x => x.RunTime)
                .HasColumnType("datetime2(0)");

            builder
                .HasDiscriminator<int>("JobType")
                .HasValue<ReccurentJobManager>(0)
                .HasValue<TimedJobManager>(1);
        }
    }
}
