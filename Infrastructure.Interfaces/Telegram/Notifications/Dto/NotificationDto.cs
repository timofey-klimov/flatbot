using System.IO;

namespace Infrastructure.Interfaces.Telegram.Dto
{
    public class NotificationDto
    {
        public string Message { get; set; }

        public byte[] Image { get; set; }

        public bool HasImage { get; set; }
    }
}
