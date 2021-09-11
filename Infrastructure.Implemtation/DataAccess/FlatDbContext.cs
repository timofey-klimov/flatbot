using Entities.Models;
using Infrastructure.Interfaces.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Implemtation.DataAccess
{
    public class FlatDbContext : DbContext, IDbContext
    {

        public FlatDbContext(DbContextOptions<FlatDbContext> options)
            : base(options)
        {

        }

        public DbSet<Flat> Flats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Flat>(x =>
            {
                x.HasKey(x => x.Id);

                x.Property(x => x.Metro)
                    .HasMaxLength(100);

                x.Property(x => x.Address)
                    .HasMaxLength(150);

                x.Property(x => x.Pledge)
                    .HasColumnType("decimal(18,2)");

                x.Property(x => x.Price)
                    .HasColumnType("decimal(18,2)");
            });
        }
    }
}
