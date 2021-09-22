using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Telegram
{
    public interface ITelegramMessageSender
    {
        Task SendMessageAsync(string message, long chatId);
    }
}
