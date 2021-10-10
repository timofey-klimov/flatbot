using Entities.Models;
using Infrastructure.Interfaces.Common;
using Infrastructure.Interfaces.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Implemtation.Common
{
    public class FilterFlatService : IFilterFlatService
    {
        private readonly IDbContext _dbContext;

        public FilterFlatService(IDbContext context)
        {
            _dbContext = context;
        }

        public async Task<ICollection<Flat>> GetFlatsByUserContextAsync(UserContext context, int takeCount, CancellationToken token = default)
        {
            if (context == null)
                throw new ArgumentException(nameof(UserContext));

            if (context.Disctricts == null)
                throw new ArgumentException(nameof(context.Disctricts));

            var flats = await _dbContext.Flats
                   .Where(x => x.Price <= context.MaximumPrice
                           && x.Price >= context.MinimumPrice
                           && x.TimeToMetro <= context.TimeToMetro
                           && x.CurrentFloor >= context.MinimumFloor
                           && (!context.Disctricts.Any() || context.Disctricts.Contains(x.District))
                           && !context.NotificationsList.Value.Contains(x.CianId))
                   .AsNoTracking()
                   .OrderBy(x => x.Price)
                   .Take(takeCount)
                   .AsNoTracking()
                   .ToListAsync(token);

            return flats;
        }

        public async Task<ICollection<Flat>> GetFlatsByUserContextAsAsyncEnumerableAsync(UserContext context, int takeCount, CancellationToken token = default)
        {
            if (context == null)
                throw new ArgumentException(nameof(UserContext));

            if (context.Disctricts == null)
                throw new ArgumentException(nameof(context.Disctricts));

            var flats = await _dbContext.Flats
                    .Where(x => x.Price <= context.MaximumPrice
                            && x.Price >= context.MinimumPrice
                            && x.TimeToMetro <= context.TimeToMetro
                            && x.CurrentFloor >= context.MinimumFloor
                            && (!context.Disctricts.Any() || context.Disctricts.Contains(x.District))
                            && !context.NotificationsList.Value.Contains(x.CianId))
                    .AsNoTracking()
                    .OrderBy(x => x.Price)
                    .Take(takeCount)
                    .ToListAsync();

            return flats;
        }
    }
}
