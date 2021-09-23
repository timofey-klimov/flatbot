using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implemtation.DataAccess.Configuration
{
    public class JobHistoryConfiguration : IEntityTypeConfiguration<JobHistory>
    {
        public void Configure(EntityTypeBuilder<JobHistory> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Message)
                .HasMaxLength(150);

            builder.Property(x => x.StartDate)
                .HasColumnType("datetime2(0)");

            builder.Property(x => x.EndDate)
                .HasColumnType("datetime2(0)");
            builder.Property(x => x.NextFireAt)
                .HasColumnType("datetime2(2)");
        }
    }
}
