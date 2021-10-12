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
                .Include(x => x.UserContext)
                .ThenInclude(x => x.Disctricts)
                .Include(x => x.NotificationContext)
                .Where(x => x.NotificationContext.IsActive
                    && x.NotificationContext.NotificationType == Entities.Enums.NotificationType.EveryWeek
                    && (x.NotificationContext.NextNotify == null || x.NotificationContext.NextNotify.Value.Day == DateTime.Now.Day))
                .ToListAsync(token);

            foreach (var user in users)
            {
                var flats = await _filter.GetFlatsByUserContextAsync(user.UserContext, _countManager.FlatCount, token);

                var notificationCreator = _creatorFactory.Create(Infrastructure.Interfaces.Telegram.Model.NotificationCreationType.WithImage);

                var messages = await notificationCreator.CreateAsync(flats);

                await _tgMessageSender.SendMessagesAsync(messages, user.ChatId);

                if (!messages.Any())
                {
                    await _tgMessageSender.SendMessageAsync("Сегодня не нашлось объявлений по твоим фильтрам", user.ChatId);
                }

                user.UserContext.AddNotifications(flats.Select(x => x.CianId));
                user.NotificationContext.CreateLastNotifyDateNow();

                ///неделя
                user.NotificationContext.SetNextNotifyDate(168);

                await _dbContext.SaveChangesAsync(token);

            }
        }
    }
}
