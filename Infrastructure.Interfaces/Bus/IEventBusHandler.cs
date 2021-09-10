using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Bus
{
    public interface IEventBusHandler<in T>
        where T : IEvent
    {
        Task Handle(T @event);
    }
}
