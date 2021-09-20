using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Cian.HttpClient
{
    public interface IClientMessageSender
    {
        Task SendMessageAsync(string message, long chatId);
    }
}
