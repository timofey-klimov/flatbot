using Infrastructure.Interfaces.Bus;
using Infrastructure.Interfaces.Cian;
using Infrastructure.Interfaces.Cian.Enums;
using Infrastructure.Interfaces.Cian.HttpClient;
using Infrastructure.Interfaces.Logger;
using System;
using System.Threading.Tasks;
using UseCases.Flats.Events;

namespace UseCases.Flats.BackgroundJobs
{
    public abstract class CianJob
    {
        protected ICianService CianService;
        protected ICianUrlBuilder CianUrlBuilder;
        protected ICianHttpClient HttpClient;
        protected ILoggerService Logger;
        protected IEventBus Bus;

        public CianJob(
            ICianService cianService,
            ICianUrlBuilder urlBuilder,
            ICianHttpClient httpClient,
            ILoggerService logger,
            IEventBus eventBus)
        {
            CianService = cianService;
            CianUrlBuilder = urlBuilder;
            HttpClient = httpClient;
            Logger = logger;
            Bus = eventBus;
        }
        protected async Task Execute(City city)
        {
            try
            {
                var pagesCount = await CianService.GetPagesCount(city);

                for (int i = 0; i < pagesCount; i++)
                {
                    try
                    {
                        var url = CianUrlBuilder.BuildCianUrl(city, OperationType.GetExcel, i);
                        var excelInBytes = await HttpClient.GetExcelFromCianAsync(url);
                        Bus.Publish(new FileSavedEvent(excelInBytes));

                        await Task.Delay(6000);
                    }
                    catch (Exception ex)
                    {
                        Logger.Error($"{ex.GetType().Name} {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"{ex.GetType().Name} {ex.Message}");
            }
        }
    }
}
