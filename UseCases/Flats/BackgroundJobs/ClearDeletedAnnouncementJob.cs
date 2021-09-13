using Infrastructure.Interfaces.Cian;
using Infrastructure.Interfaces.DataAccess;
using Infrastructure.Interfaces.Jobs;
using Infrastructure.Interfaces.Logger;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace UseCases.Flats.BackgroundJobs
{
    public class ClearDeletedAnnouncementJob : IJob
    {
        private readonly IDbContext _dbContext;
        private readonly ICianService _cianService;
        private readonly ILoggerService _logger;

        public ClearDeletedAnnouncementJob(
            IDbContext dbContext,
            ICianService cianService,
            ILoggerService logger
            )
        {
            _dbContext = dbContext;
            _cianService = cianService;
            _logger = logger;
        }
        public async Task Execute(CancellationToken token)
        {
            var dtos = _dbContext.Flats
                .Select(x => new { x.Id, x.CianReference })
                .AsNoTracking()
                .ToArray();

            foreach (var dto in dtos)
            {
                try
                {
                    if (token.IsCancellationRequested)
                        break;

                    var check = await _cianService.CheckAnnouncement(dto.CianReference);

                    if (check)
                    {
                        var entity = await _dbContext.Flats.FirstOrDefaultAsync(x => x.Id == dto.Id);

                        _dbContext.Flats.Remove(entity);

                        _logger.Info($"Announcement deleted {dto.CianReference}");
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error(ex.Message);
                }
            }
        }
    }
}
