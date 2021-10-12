using Infrastructure.Interfaces.Bus;
using Infrastructure.Interfaces.Cian;
using Infrastructure.Interfaces.Cian.Enums;
using Infrastructure.Interfaces.Cian.Events.ExcelDownloaded;
using Infrastructure.Interfaces.Cian.Events.FinishParseCian;
using Infrastructure.Interfaces.DataAccess;
using Infrastructure.Interfaces.Logger;
using System;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Flats.BackgroundJobs.Exceptions;

namespace UseCases.Flats.BackgroundJobs
{
    public abstract class CianJob
    {
        protected IParseCianManager ParseCianManager;
        protected ILoggerService Logger;
        protected IEventBus Bus;
        protected IDbContext DbContext;
        protected ICianUrlBuilder UrlBuilder;

        public CianJob(
            IParseCianManager cianService,
            ILoggerService logger,
            IEventBus eventBus,
            IDbContext dbContext,
            ICianUrlBuilder urlBuilder)
        {
            ParseCianManager = cianService;
            Logger = logger;
            Bus = eventBus;
            DbContext = dbContext;
            UrlBuilder = urlBuilder;
        }
        protected async Task Execute(City city, CancellationToken token)
        {
            var pagesCount = await ParseCianManager.GetPagesCountAsync(city);

            Logger.Info($"Find {pagesCount} pages");

            if (pagesCount == 0)
                throw new FindZeroPagesException("Cant find count of pages");

            await DbContext.ClearTableFlats(token);

            for (int i = 0; i < pagesCount; i++)
            {
                Logger.Info($"Start {i} page");

                try
                {
                    if (token.IsCancellationRequested)
                        break;

                    var url = UrlBuilder.BuildCianUrl(city, i);
                    var html = await ParseCianManager.GetHtmlAsync(url);
                    Bus.Publish(new HtmlDownloadedEvent(html));

                    await Task.Delay(6000);
                }
                catch (Exception ex)
                {
                    Logger.Error($"{ex.GetType().Name} {ex.Message}");
                }
            }

            Bus.Publish(new FinishParseCianEvent());
        }
    }
}
