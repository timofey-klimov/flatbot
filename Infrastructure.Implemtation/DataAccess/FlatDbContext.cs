using Entities.Models;
using Infrastructure.Interfaces.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
