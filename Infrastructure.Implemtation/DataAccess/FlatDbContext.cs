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

        public DbSet<JobHistory> JobHistory { get; set; }

        public DbSet<Proxy> Proxies { get; set; }

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

                x.Property(x => x.Comission)
                    .HasColumnType("int");
            });

            modelBuilder.Entity<JobHistory>(x =>
            {
                x.HasKey(x => x.Id);

                x.Property(x => x.Message)
                    .HasMaxLength(150);

                x.Property(x => x.StartDate)
                    .HasColumnType("datetime2(0)");

                x.Property(x => x.EndDate)
                    .HasColumnType("datetime2(0)");
            });

            modelBuilder.Entity<Proxy>(x =>
            {
                x.HasKey(x => x.Id);

                x.Property(x => x.Ip)
                    .HasMaxLength(50);

                x.Property(x => x.Port)
                    .HasColumnType("int");

            });
        }
    }
}
