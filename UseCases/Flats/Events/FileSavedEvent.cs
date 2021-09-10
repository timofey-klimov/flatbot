using Infrastructure.Interfaces.Bus;

namespace UseCases.Flats.Events
{
    public class FileSavedEvent : IEvent
    {
        public byte[] FileInBytes { get; }
        public FileSavedEvent(byte[] bytes)
        {
            FileInBytes = bytes;
        }
    }
}
