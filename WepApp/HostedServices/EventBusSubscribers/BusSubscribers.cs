using Infrastructure.Implemtation.Common.EventHandlers;
using Infrastructure.Interfaces.Bus;
using Infrastructure.Interfaces.Common.Events;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WepApp.HostedServices.EventBusSubscribers
{
    public class BusSubscribers : IHostedService
    {
        private readonly IEventBus _eventBus;

        public BusSubscribers(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _eventBus.Subscribe<NewFlatCreatedHandler, NewFlatCreatedEvent>();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _eventBus.Dispose();
        }
    }
}
