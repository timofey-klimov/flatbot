using Infrastructure.Interfaces.Telegram.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Telegram
{
    public interface ITelegramMessageSender
    {
        Task SendMessageAsync(string message, long chatId);

        Task SendMessagesAsync(ICollection<NotificationDto> items, long chatId);
    }
}
