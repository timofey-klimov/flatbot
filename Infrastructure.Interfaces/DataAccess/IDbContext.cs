using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.DataAccess
{
    public interface IDbContext
    {
        DbSet<Flat> Flats { get; }
        public DatabaseFacade Database { get; }
        Task<int> SaveChangesAsync(CancellationToken token);
    }
}
