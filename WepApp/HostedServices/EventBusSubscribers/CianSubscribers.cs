using Infrastructure.Implemtation.Cian.EventHandlers;
using Infrastructure.Interfaces.Bus;
using Infrastructure.Interfaces.Cian.Events.ExcelDownloaded;
using Infrastructure.Interfaces.Cian.Events.FinishParseCian;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace WepApp.HostedServices.EventBusSubscribers
{
    public class CianSubscribers : IHostedService
    {
        private IEventBus _bus;
        public CianSubscribers(
            IEventBus bus)
        {
            _bus = bus;
        }


        public Task StartAsync(CancellationToken cancellationToken)
        {
            _bus.Subscribe<HtmlDownloadHandler, HtmlDownloadedEvent>();
            _bus.Subscribe<SendNotificationsHandler, FinishParseCianEvent>();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _bus.Dispose();

            return Task.CompletedTask;
        }
    }
}
