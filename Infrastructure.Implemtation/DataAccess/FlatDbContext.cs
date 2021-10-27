using Entities.Models;
using Entities.Models.Base;
using Entities.Models.FlatEntities;
using Infrastructure.Interfaces.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Implemtation.DataAccess
{
    public class FlatDbContext : DbContext, IDbContext
    {

        public FlatDbContext(DbContextOptions<FlatDbContext> options)
            : base(options)
        {

        }

        public DbSet<Flat> Flats { get; set; }
        public DbSet<Entities.Models.Proxy> Proxies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<SheduleJobManager> SheduleJobManagers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var entites = this.ChangeTracker
                    .Entries()
                    .ToList()
                    .Where(x => typeof(JsonPropertyEntity).IsAssignableFrom(x.Entity.GetType()))
                    .ToList();

                entites.ForEach(x =>
                {
                    var e = (JsonPropertyEntity)x.Entity;
                    e.UpdateJsonEntity();
                });

                return base.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task ClearTableFlats(CancellationToken token = default)
        {
            await this.Database.ExecuteSqlRawAsync("TRUNCATE TABLE dbo.Flats", token);
        }
    }
}
