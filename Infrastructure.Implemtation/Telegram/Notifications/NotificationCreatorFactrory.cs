using Infrastructure.Implemtation.Telegram.NotificationCreators;
using Infrastructure.Interfaces.DataAccess;
using Infrastructure.Interfaces.Logger;
using Infrastructure.Interfaces.Telegram.Base;
using Infrastructure.Interfaces.Telegram.Model;
using System;

namespace Infrastructure.Implemtation.Telegram.Factory
{
    public class NotificationCreatorFactrory : INotificationCreatorFactory
    {
        private readonly IDbContext dbContext;
        private readonly ILoggerService logger;
        public NotificationCreatorFactrory(IDbContext dbContext, ILoggerService logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public INotificationCreator Create(NotificationCreationType type)
        {
            switch (type)
            {
                case NotificationCreationType.Default:
                    return new DefaultNotificationCreator();
                case NotificationCreationType.WithImage:
                    return new NotificationWithMessageCreator(dbContext, logger);
            }
            throw new ArgumentException($"No such type {nameof(NotificationCreationType)}");
        }
    }
}
