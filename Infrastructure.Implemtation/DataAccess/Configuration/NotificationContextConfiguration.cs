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
    public class NotificationContextConfiguration : IEntityTypeConfiguration<NotificationContext>
    {
        public void Configure(EntityTypeBuilder<NotificationContext> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.LastNotify)
                .HasColumnType("datetime2(0)");

            builder.Property(x => x.NextNotify)
                .HasColumnType("datetime2(0)");
        }
    }
}
