using Infrastructure.Interfaces.Bus;
using Infrastructure.Interfaces.Logger;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Implemtation.Bus
{
    public class InMemoryBus : IEventBus
    {
        private ConcurrentDictionary<EventSubscriber, Type> _events;

        private ILoggerService _logger;
        private IServiceScopeFactory _factory;

        public InMemoryBus(
            ILoggerService logger,
            IServiceScopeFactory  factory)
        {
            _events = new ConcurrentDictionary<EventSubscriber, Type>();
            _logger = logger;
            _factory = factory;
        }

        public void Publish(IEvent @event)
        {
            _logger.Debug($"Publish event {@event.GetType().Name}");

            var messageType = @event.GetType();

            var handler = _events.Where(x => x.Key.Message == messageType)
                .Select(x => x.Value)
                .FirstOrDefault();

            using (var scope = _factory.CreateScope())
            {
                ///У обработчиков событий всегда есть метод Handle, который принимает объект типа IEvent
                dynamic service = scope.ServiceProvider.GetService(handler);

                service.Handle(@event);
            }
        }

        public IDisposable Subscribe<T, V>()
            where T : IEventBusHandler<V>
            where V : IEvent
        {
            var disposer = new EventSubscriber(typeof(V), x => _events.Remove(x, out var _));

            _events.TryAdd(disposer, typeof(T));

            return disposer;
        }
    }
}
