using Infrastructure.Interfaces.Bus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace WepApp.HostedServices.EventBusSubscribers
{
    public abstract class EventBusSubscriberBase<T> : IHostedService
        where T : IEventBusHandler
    {
        protected IEventBus EventBus;
        protected T ServiceHandler;
        public EventBusSubscriberBase(
            IEventBus bus,
            IServiceScopeFactory factory)
        {
            EventBus = bus;
            using(var scope = factory.CreateScope())
            {
                ServiceHandler = scope.ServiceProvider.GetRequiredService<T>();
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Dispose();
            return Task.CompletedTask;
        }

        public abstract void Dispose();
    }
}
