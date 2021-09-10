using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.DataAccess
{
    public interface IDbContext : IDisposable
    {
        DbSet<Flat> Flats { get; }
        public DatabaseFacade Database { get; }
        Task<int> SaveChangesAsync(CancellationToken token);
    }
}
