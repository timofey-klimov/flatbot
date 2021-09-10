using Infrastructure.Interfaces.Bus;
using Infrastructure.Interfaces.Cian;
using Microsoft.Extensions.DependencyInjection;
using System;
using UseCases.Flats.Events;

namespace WepApp.HostedServices.EventBusSubscribers
{
    public class CianExcelParserHostedService : EventBusSubscriberBase<ICianExcelParser>
    {
        private IDisposable disposer;
        public CianExcelParserHostedService(
            IEventBus bus,
            IServiceScopeFactory factory)
            : base(bus, factory)
        {
            disposer = EventBus.Subscribe<FileSavedEvent>(x => ServiceHandler.ParseAsync(x.FileInBytes));
        }

        public override void Dispose()
        {
            disposer.Dispose();
        }
    }
}
