using Entities.Models;
using Infrastructure.Interfaces.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;

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

        public DbSet<User> Users { get;set; }

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
                x.Property(x => x.NextFireAt)
                    .HasColumnType("datetime2(2)");
            });

            modelBuilder.Entity<Proxy>(x =>
            {
                x.HasKey(x => x.Id);

                x.Property(x => x.Ip)
                    .HasMaxLength(50);

                x.Property(x => x.Port)
                    .HasColumnType("int");

            });
            modelBuilder.Entity<User>(x =>
            {
                x.HasKey(x => x.Id);
                x.Property(x => x.UserName)
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<UserContext>(x =>
            {
                x.HasKey(x => x.Id);

                x.Property(x => x.MinimumPrice)
                    .HasColumnType("decimal(18,2)");

                x.Property(x => x.MaximumPrice)
                    .HasColumnType("decimal(18,2)");

                x.Property(x => x.PostedNotifications)
                    .HasColumnType("nvarchar(max)");

                x.Ignore(x => x.NotificationsList);
                
            });

            modelBuilder.Entity<NotificationContext>(x =>
            {
                x.HasKey(x => x.Id);

                x.Property(x => x.LastNotify)
                    .HasColumnType("datetime2(0)");

                x.Property(x => x.NextNotify)
                    .HasColumnType("datetime2(0)");
            });
        }
    }
}
