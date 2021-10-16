using Infrastructure.Interfaces.Telegram.Dto;
using Infrastructure.Interfaces.Telegram.MessageSender;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Telegram
{
    public interface ITelegramMessageSender
    {
        Task<TelegramMessageSenderResult> SendMessageAsync(string message, long chatId);

        Task<TelegramMessageSenderResult> SendMessagesAsync(ICollection<NotificationDto> items, long chatId);
    }
}
