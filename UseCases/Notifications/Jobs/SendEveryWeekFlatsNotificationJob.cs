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
        private readonly ITelegramNotificationService _tgNotifyService;
        private readonly ITelegramMessageSender _tgMessageSender;

        public SendEveryWeekFlatsNotificationJob(
            IDbContext dbContext,
            ITelegramNotificationService tgNotifyService,
            ITelegramMessageSender tgMessageSender)
        {
            _dbContext = dbContext;
            _tgMessageSender = tgMessageSender;
            _tgNotifyService = tgNotifyService;
        }

        public async Task ExecuteAsync(CancellationToken token = default)
        {
            var users = await _dbContext.Users
                .Include(x => x.UserContext)
                .Include(x => x.NotificationContext)
                .Where(x => x.NotificationContext.IsActive
                    && x.NotificationContext.NotificationType == Entities.Enums.NotificationType.EveryWeek
                    && (x.NotificationContext.NextNotify == null || x.NotificationContext.NextNotify.Value.Day == DateTime.Now.Day))
                .ToListAsync(token);

            foreach (var user in users)
            {
                var flats = await _dbContext.Flats
                    .Where(x => x.Price <= user.UserContext.MaximumPrice
                            && x.Price >= user.UserContext.MinimumPrice
                            && x.TimeToMetro <= user.UserContext.TimeToMetro)
                    .AsNoTracking()
                    .OrderBy(x => x.Price)
                    .Take(45)
                    .ToListAsync(token);

                var messages = _tgNotifyService.CreateMany(flats, 15);

                if (!messages.Any())
                {
                    await _tgMessageSender.SendMessageAsync("Сегодня не нашлось объявлений по твоим фильтрам", user.ChatId);
                    continue;
                }

                foreach (var message in messages)
                {
                    await _tgMessageSender.SendMessageAsync(message, user.ChatId);
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
