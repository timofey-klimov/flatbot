using Infrastructure.Interfaces.Bus;
using Infrastructure.Interfaces.Cian.Events.FinishParseCian;
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
        private readonly ITelegramNotificationService _tgNotifyService;

        public SendNotificationsHandler(
            IDbContext dbContext,
            ITelegramMessageSender messageSender,
            ITelegramNotificationService tgNotifyService) 
        {
            _dbContext = dbContext;
            _messageSender = messageSender;
            _tgNotifyService = tgNotifyService;
        }

        public async Task HandleAsync(FinishParseCianEvent @event)
        {
            var users = await _dbContext.Users
                .Include(x => x.NotificationContext)
                .Include(x => x.UserContext)
                .ThenInclude(x => x.Disctricts)
                .Where(x => x.NotificationContext.IsActive == true && x.NotificationContext.NotificationType == Entities.Enums.NotificationType.Default)
                .ToListAsync();

            foreach (var user in users)
            {
                var flats = await _dbContext.Flats
                    .Where(x => x.Price <= user.UserContext.MaximumPrice
                            && x.Price >= user.UserContext.MinimumPrice
                            && x.TimeToMetro <= user.UserContext.TimeToMetro
                            && x.CurrentFloor >= user.UserContext.MinimumFloor
                            && user.UserContext.Disctricts.Contains(x.District)
                            && !user.UserContext.NotificationsList.Value.Contains(x.CianId))
                    .AsNoTracking()
                    .OrderBy(x => x.Price)
                    .Take(30)
                    .ToListAsync();

                if (!flats.Any())
                    continue;

                var messages = _tgNotifyService.CreateMany(flats, 15);

                foreach (var message in messages)
                {
                    await _messageSender.SendMessageAsync(message, user.ChatId);
                    await Task.Delay(1000);
                }

                var ciandIds = flats.Select(x => x.CianId);

                user.UserContext.AddNotifications(ciandIds);
                user.NotificationContext.CreateLastNotifyDateNow();

                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
