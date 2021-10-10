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
    public class SendEveryDayFlatsNotificationJob : IJob
    {
        private readonly IDbContext _dbContext;
        private readonly ITelegramNotificationCreator _tgNotifyService;
        private readonly ITelegramMessageSender _tgMessageSender;
        private readonly IFilterFlatService _filter;
        private readonly IFlatCountInMessageManager _countManager;

        public SendEveryDayFlatsNotificationJob(
            IDbContext dbContext,
            ITelegramNotificationCreator tgNotifyService,
            ITelegramMessageSender tgMessageSender,
            IFilterFlatService filter,
            IFlatCountInMessageManager countManager)
        {
            _dbContext = dbContext;
            _tgNotifyService = tgNotifyService;
            _tgMessageSender = tgMessageSender;
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
                && x.NotificationContext.NotificationType == Entities.Enums.NotificationType.EveryDay
                && (x.NotificationContext.NextNotify == null || x.NotificationContext.NextNotify.Value.Day == DateTime.Now.Day))
                .ToListAsync(token);

            foreach (var user in users)
            {
                var flats = await _filter.GetFlatsByUserContextAsync(user.UserContext, _countManager.FlatCount, token);

                var messages = _tgNotifyService.CreateMessages(flats, flats.Count);

                foreach (var message in messages)
                {
                    await _tgMessageSender.SendMessageAsync(message, user.ChatId);
                    await Task.Delay(1000);
                }

                if (!messages.Any())
                {
                    await _tgMessageSender.SendMessageAsync("Сегодня не нашлось объявлений по твоим фильтрам", user.ChatId);
                }

                user.UserContext.AddNotifications(flats.Select(x => x.CianId));

                user.NotificationContext.SetNextNotifyDate(24);

                user.NotificationContext.CreateLastNotifyDateNow();

                await _dbContext.SaveChangesAsync(token);
            }
        }
    }
}
