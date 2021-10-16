using Infrastructure.Implemtation.Telegram.Factory;
using Infrastructure.Interfaces.Bus;
using Infrastructure.Interfaces.Cian.Events.FinishParseCian;
using Infrastructure.Interfaces.Common;
using Infrastructure.Interfaces.DataAccess;
using Infrastructure.Interfaces.Telegram;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Implemtation.Cian.EventHandlers
{
    public class SendNotificationsHandler : IEventBusHandler<FinishParseCianEvent>
    {
        private readonly IDbContext _dbContext;
        private readonly ITelegramMessageSender _messageSender;
        private readonly INotificationCreatorFactory _creator;
        private readonly IFilterFlatService _filter;
        private readonly IFlatCountInMessageManager _countManager;

        public SendNotificationsHandler(
            IDbContext dbContext,
            ITelegramMessageSender messageSender,
            INotificationCreatorFactory creator,
            IFilterFlatService filter,
            IFlatCountInMessageManager countManager) 
        {
            _dbContext = dbContext;
            _messageSender = messageSender;
            _creator = creator;
            _filter = filter;
            _countManager = countManager;
        }

        public async Task HandleAsync(FinishParseCianEvent @event)
        {
            var users = await _dbContext.Users
                .Where(x => x.NotificationContext.IsActive == true && x.NotificationContext.NotificationType == Entities.Enums.NotificationType.Default)
                .Include(x => x.NotificationContext)
                .Include(x => x.UserContext)
                .ThenInclude(x => x.Disctricts)
                .ToListAsync();

            foreach (var user in users)
            {
                var flats = await _filter.GetFlatsByUserContextAsync(user.UserContext, _countManager.FlatCount);

                if (!flats.Any())
                    continue;

                var notificationCreator = _creator.Create(Interfaces.Telegram.Model.NotificationCreationType.WithImage);

                var messages = await notificationCreator.CreateAsync(flats);

                var result = await _messageSender.SendMessagesAsync(messages, user.ChatId);

                if (result.Success)
                    user.UserContext.AddNotifications(flats.Select(x => x.CianId));

                user.NotificationContext.CreateLastNotifyDateNow();

                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
