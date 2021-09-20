using System;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Bus
{
    public interface IEventBus : IDisposable
    {
        Task Publish(IEvent @event);

        IDisposable Subscribe<T, V>()
            where T : IEventBusHandler<V>
            where V : IEvent;
    }
}
