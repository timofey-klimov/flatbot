﻿using Infrastructure.Interfaces.Bus;
using Infrastructure.Interfaces.Cian;
using Infrastructure.Interfaces.Cian.Enums;
using Infrastructure.Interfaces.Cian.Events.ExcelDownloaded;
using Infrastructure.Interfaces.Logger;
using System;
using System.Threading.Tasks;

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
        protected async Task Execute(City city)
        {
            try
            {
                var pagesCount = await CianService.GetPagesCountAsync(city);

                for (int i = 0; i < pagesCount; i++)
                {
                    Logger.Debug($"Start {i} page");

                    try
                    {
                        var url = CianService.BuildCianUrl(city, OperationType.GetExcel, i);
                        var excelInBytes = await CianService.GetExcelFromCianAsync(url);
                        Bus.Publish(new ExcelDownloadedEvent(excelInBytes));

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