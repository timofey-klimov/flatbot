using Entities.Models;
using Entities.Models.FlatEntities;
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
                .Include(x => x.Address)
                .ThenInclude(x => x.Okrug)
                .Include(x => x.BuildingInfo)
                .Include(x => x.FlatGeo)
                .Include(x => x.ParkingInfo)
                .Include(x => x.PhotoInfos)
                .Include(x => x.RailwayInfos)
                .Include(x => x.UndergroundInfos)
                .Include(x => x.PriceInfo)
                .Where(x => x.PriceInfo.Price <= context.MaximumPrice
                        && x.PriceInfo.Price >= context.MinimumPrice
                        && x.UndergroundInfos.Where(x => x.Time <= context.TimeToMetro) != null
                        && x.CurrentFloor >= context.MinimumFloor
                        && (!context.Disctricts.Any() || context.Disctricts.Contains(x.Address.Okrug))
                        && !context.NotificationsList.Value.Contains(x.CianId))
                .AsNoTracking()
                .OrderBy(x => x.PriceInfo.Price)
                .Take(takeCount)
                .AsNoTracking()
                .ToListAsync(token);

            return flats;
        }
    }
}
