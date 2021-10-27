using Infrastructure.Interfaces.Bus;

namespace Infrastructure.Interfaces.Common.Events
{
    public class NewFlatCreatedEvent : IEvent
    {
        public int Id { get; }
        public NewFlatCreatedEvent(int id)
        {
            Id = id;
        }
    }
}
