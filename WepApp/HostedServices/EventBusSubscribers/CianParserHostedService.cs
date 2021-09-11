using Infrastructure.Implemtation.Cian.EventHandlers;
using Infrastructure.Interfaces.Bus;
using Infrastructure.Interfaces.Cian.Events.ExcelDownloaded;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace WepApp.HostedServices.EventBusSubscribers
{
    public class CianParserHostedService : IHostedService
    {
        private IDisposable disposer;
        public CianParserHostedService(
            IEventBus bus)
        {
            disposer = bus.Subscribe<HtmlDownloadHandler, HtmlDownloadedEvent>();
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
