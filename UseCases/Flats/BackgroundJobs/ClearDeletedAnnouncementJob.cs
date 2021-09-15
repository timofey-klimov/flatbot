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
                .Select(x => new { x.CianId, x.CianReference })
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
                        var entities = await _dbContext.Flats.Where(x => dto.CianId == x.CianId).ToListAsync();

                        _dbContext.Flats.RemoveRange(entities);

                        await _dbContext.SaveChangesAsync();

                        _logger.Info($"Announcement deleted {dto.CianReference}");
                    }

                    await Task.Delay(6000);
                }
                catch (Exception ex)
                {
                    _logger.Error(ex.Message);
                }
            }
        }
    }
}
