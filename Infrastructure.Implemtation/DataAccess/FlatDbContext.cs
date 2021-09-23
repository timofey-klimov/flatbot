using Entities.Models;
using Infrastructure.Interfaces.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

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
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
