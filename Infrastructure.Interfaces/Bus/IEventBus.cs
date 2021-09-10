using System;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Bus
{
    public interface IEventBus
    {
        void Publish(IEvent @event);

        IDisposable Subscribe<T>(Func<T, Task> func) where T : IEvent;
    }
}
