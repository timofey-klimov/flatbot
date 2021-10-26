using Infrastructure.Interfaces.Cian;
using Infrastructure.Interfaces.Cian.Dto;
using Infrastructure.Interfaces.DataAccess;
using Infrastructure.Interfaces.Logger;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implemtation.Cian
{
    public class CianFlatsCreator : ICianFlatsCreator
    {
        private readonly IDbContext _dbContext;
        private readonly ICianFlatJsonCreator _jsonCreator;
        private readonly ICianFlatJsonParser _jsonParser;
        private readonly ILoggerService _logger;

        public CianFlatsCreator(
            IDbContext dbContext,
            ICianFlatJsonParser cianFlatJsonParser,
            ICianFlatJsonCreator cianFlatJsonCreator,
            ILoggerService logger) 
        {
            _dbContext = dbContext;
            _jsonCreator = cianFlatJsonCreator;
            _jsonParser = cianFlatJsonParser;
            _logger = logger;
        }

        public async Task CreateAsync(IEnumerable<FindedFlatDto> flats)
        {
            foreach (var flat in flats)
            {
                try
                {
                    var dbFlat = await _dbContext.Flats.FirstOrDefaultAsync(x => x.CianId == flat.CianId);

                    if (dbFlat != null)
                        continue;

                    var json = await _jsonCreator.CreateAsync(flat.CianUrl);

                    await _jsonParser.ParseAsync(json, flat);
                }
                catch (Exception ex)
                {
                    _logger.Error(this.GetType(), ex.Message);
                }
            }
        }
    }
}
