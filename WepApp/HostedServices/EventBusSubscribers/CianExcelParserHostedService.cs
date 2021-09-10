using Infrastructure.Implemtation.Cian.Events.ExcelDownloaded;
using Infrastructure.Interfaces.Bus;
using Infrastructure.Interfaces.Cian.Events.ExcelDownloaded;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace WepApp.HostedServices.EventBusSubscribers
{
    public class CianExcelParserHostedService : IHostedService
    {
        private IDisposable disposer;
        public CianExcelParserHostedService(
            IEventBus bus)
        {
            disposer = bus.Subscribe<ExcelDownloadHandler, ExcelDownloadedEvent>();
        }


        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            disposer.Dispose();
            return Task.CompletedTask;
        }
    }
}
