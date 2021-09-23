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
    public class FlatConfiguration : IEntityTypeConfiguration<Flat>
    {
        public void Configure(EntityTypeBuilder<Flat> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Metro)
                .HasMaxLength(100);

            builder.Property(x => x.Address)
                .HasMaxLength(150);

            builder.Property(x => x.Pledge)
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.Price)
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.Comission)
                .HasColumnType("int");
        }
    }
}
