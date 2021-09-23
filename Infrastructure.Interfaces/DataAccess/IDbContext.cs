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

        DbSet<JobHistory> JobHistory { get; }

        DbSet<Proxy> Proxies { get; }

        DbSet<User> Users { get; }

        DbSet<District> Districts { get; }

        DatabaseFacade Database { get; }
        Task<int> SaveChangesAsync(CancellationToken token = default);
    }
}
