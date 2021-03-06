using Infrastructure.Implemtation.Telegram.Factory;
using Infrastructure.Interfaces.Common;
using Infrastructure.Interfaces.DataAccess;
using Infrastructure.Interfaces.Jobs;
using Infrastructure.Interfaces.Telegram;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace UseCases.Notifications.Jobs
{
    public class SendEveryWeekFlatsNotificationJob : IJob
    {
        private readonly IDbContext _dbContext;
        private readonly INotificationCreatorFactory _creatorFactory;
        private readonly ITelegramMessageSender _tgMessageSender;
        private readonly IFilterFlatService _filter;
        private readonly IFlatCountInMessageManager _countManager;

        public SendEveryWeekFlatsNotificationJob(
            IDbContext dbContext,
            INotificationCreatorFactory creatorFactory,
            ITelegramMessageSender tgMessageSender,
            IFilterFlatService filter,
            IFlatCountInMessageManager countManager)
        {
            _dbContext = dbContext;
            _tgMessageSender = tgMessageSender;
            _creatorFactory = creatorFactory;
            _filter = filter;
            _countManager = countManager;
        }

        public async Task ExecuteAsync(CancellationToken token = default)
        {
            var users = await _dbContext.Users
                .Where(x => x.NotificationContext.IsActive
                    && x.NotificationContext.NotificationType == Entities.Enums.NotificationType.EveryWeek
                    &&(!x.NotificationContext.NextNotify.HasValue
                        ||
                    ((x.NotificationContext.NextNotify.Value.Day == DateTime.Now.Day
                         &&
                         x.NotificationContext.NextNotify.Value.Month == DateTime.Now.Month))))
                .Include(x => x.UserContext)
                .ThenInclude(x => x.Districts)
                .Include(x => x.NotificationContext)
                .ToListAsync(token);

            foreach (var user in users)
            {
                var flats = await _filter.GetFlatsByUserContextAsync(user.UserContext, _countManager.FlatCount, token);

                var notificationCreator = _creatorFactory.Create(Infrastructure.Interfaces.Telegram.Model.NotificationCreationType.WithImage);

                var messages = await notificationCreator.CreateAsync(flats, user.ChatId);

                var result =  await _tgMessageSender.SendMessagesAsync(messages, user.ChatId);

                if (!messages.Any())
                {
                    _ = await _tgMessageSender.SendMessageAsync("Сегодня не нашлось объявлений по твоим фильтрам", user.ChatId);
                }

                if (result.Success)
                    user.UserContext.UpdateNotifications(flats.Select(x => x.CianId));

                user.NotificationContext.CreateLastNotifyDateNow();

                ///неделя
                user.NotificationContext.SetNextNotifyDate(168);

                await _dbContext.SaveChangesAsync(token);

            }
        }
    }
}
