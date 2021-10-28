using Infrastructure.Implemtation.Telegram.NotificationCreators;
using Infrastructure.Interfaces.DataAccess;
using Infrastructure.Interfaces.Logger;
using Infrastructure.Interfaces.Telegram.Base;
using Infrastructure.Interfaces.Telegram.HostManager;
using Infrastructure.Interfaces.Telegram.Model;
using System;

namespace Infrastructure.Implemtation.Telegram.Factory
{
    public class NotificationCreatorFactrory : INotificationCreatorFactory
    {
        private readonly IDbContext dbContext;
        private readonly ILoggerService logger;
        private readonly ITelegramClientHostManager hostManager;
        public NotificationCreatorFactrory(
            IDbContext dbContext, 
            ILoggerService logger,
            ITelegramClientHostManager hostManager)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.hostManager = hostManager;
        }

        public INotificationCreator Create(NotificationCreationType type)
        {
            switch (type)
            {
                case NotificationCreationType.Default:
                    return new DefaultNotificationCreator(hostManager);
                case NotificationCreationType.WithImage:
                    return new NotificationWithMessageCreator(dbContext, logger, hostManager);
            }
            throw new ArgumentException($"No such type {nameof(NotificationCreationType)}");
        }
    }
}
