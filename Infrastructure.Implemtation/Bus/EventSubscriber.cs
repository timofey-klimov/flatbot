using System;

namespace Infrastructure.Implemtation.Bus
{
    public class EventSubscriber : IDisposable
    {
        private Action<EventSubscriber> _disposeAction;
        public Type Message { get; }

        public EventSubscriber(Type message, Action<EventSubscriber> disposeAction)
        {
            Message = message;
            _disposeAction = disposeAction;
        }

        public void Dispose()
        {
            _disposeAction.Invoke(this);
        }
    }
}
