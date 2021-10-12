using Infrastructure.Interfaces.Telegram.Base;
using Infrastructure.Interfaces.Telegram.Model;

namespace Infrastructure.Implemtation.Telegram.Factory
{
    public interface INotificationCreatorFactory
    {
        INotificationCreator Create(NotificationCreationType type);
    }
}
