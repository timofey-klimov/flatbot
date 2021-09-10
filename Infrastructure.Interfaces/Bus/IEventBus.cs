using System;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Bus
{
    public interface IEventBus
    {
        void Publish(IEvent @event);

        IDisposable Subscribe<T, V>()
             where T : IEventBusHandler<V>
             where V : IEvent;
    }
}
