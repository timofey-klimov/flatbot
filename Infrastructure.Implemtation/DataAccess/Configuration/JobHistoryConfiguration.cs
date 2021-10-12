using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Implemtation.DataAccess.Configuration
{
    public class JobHistoryConfiguration : IEntityTypeConfiguration<JobHistory>
    {
        public void Configure(EntityTypeBuilder<JobHistory> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FinishTime)
                .HasColumnType("datetime2(0)");
        }
    }
}
