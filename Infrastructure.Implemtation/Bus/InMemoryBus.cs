using Infrastructure.Interfaces.Bus;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Implemtation.Bus
{
    public class InMemoryBus : IEventBus
    {
        private ConcurrentDictionary<EventSubscriber, Func<IEvent, Task>> _events;

        public InMemoryBus()
        {
            _events = new ConcurrentDictionary<EventSubscriber, Func<IEvent, Task>>();
        }

        public void Publish(IEvent @event)
        {
            var messageType = @event.GetType();

            var handlers = _events.Where(x => x.Key.Message == messageType)
                .Select(x => x.Value(@event));

            Task.WhenAll(handlers);
        }

        public IDisposable Subscribe<T>(Func<T, Task> func) where T : IEvent
        {
            var disposer = new EventSubscriber(typeof(T), x => _events.Remove(x, out var _));

            _events.TryAdd(disposer, x => func((T)x));

            return disposer;
        }
    }
}
