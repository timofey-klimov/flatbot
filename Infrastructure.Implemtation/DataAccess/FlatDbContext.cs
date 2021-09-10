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

                x.OwnsOne(x => x.Metro, c =>
                {
                    c.Property(x => x.Name).HasMaxLength(100)
                        .HasColumnName("MetroName");
                    c.Property(x => x.OnCar)
                        .HasColumnName("OnCar");
                    c.Property(x => x.TimeToGoInMinutes)
                        .HasColumnName("TimeToGo");
                });

                x.OwnsOne(x => x.Address, c =>
                {
                    c.Property(x => x.Value)
                        .HasColumnName("Address")
                        .HasMaxLength(200);
                });

                x.OwnsOne(x => x.Price, c =>
                {
                    c.Property(x => x.Value)
                        .HasColumnName("Price")
                        .HasColumnType("decimal(18,2)")
                        .IsRequired();
                });

                x.Property(x => x.Phone)
                    .HasMaxLength(100);
            });
        }
    }
}
