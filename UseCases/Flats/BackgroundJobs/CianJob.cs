using Infrastructure.Interfaces.Bus;
using Infrastructure.Interfaces.Cian;
using Infrastructure.Interfaces.Cian.Enums;
using Infrastructure.Interfaces.Cian.Events.ExcelDownloaded;
using Infrastructure.Interfaces.Logger;
using System;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Flats.BackgroundJobs.Exceptions;

namespace UseCases.Flats.BackgroundJobs
{
    public abstract class CianJob
    {
        protected ICianService CianService;
        protected ILoggerService Logger;
        protected IEventBus Bus;

        public CianJob(
            ICianService cianService,
            ILoggerService logger,
            IEventBus eventBus)
        {
            CianService = cianService;
            Logger = logger;
            Bus = eventBus;
        }
        protected async Task Execute(City city, CancellationToken token)
        {
            var pagesCount = await CianService.GetPagesCountAsync(city);

            Logger.Info($"Find {pagesCount} pages");

            if (pagesCount == 0)
                throw new FindZeroPagesException("Cant find count of pages");

            for (int i = 0; i < pagesCount; i++)
            {
                Logger.Info($"Start {i} page");

                try
                {
                    if (token.IsCancellationRequested)
                        break;

                    var url = CianService.BuildCianUrl(city, i);
                    var html = await CianService.GetHtmlAsync(url);
                    Bus.Publish(new HtmlDownloadedEvent(html));

                    await Task.Delay(6000);
                }
                catch (Exception ex)
                {
                    Logger.Error($"{ex.GetType().Name} {ex.Message}");
                }
            }
        }
    }
}
