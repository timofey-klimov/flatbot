using Infrastructure.Interfaces.Bus;
using Infrastructure.Interfaces.Cian.Events.FinishParseCian;
using Infrastructure.Interfaces.Cian.HttpClient;
using Infrastructure.Interfaces.DataAccess;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implemtation.Cian.EventHandlers
{
    public class SendNotificationsHandler : IEventBusHandler<FinishParseCianEvent>
    {
        private readonly IDbContext _dbContext;
        private readonly IClientMessageSender _messageSender;

        public SendNotificationsHandler(
            IDbContext dbContext,
            IClientMessageSender messageSender) 
        {
            _dbContext = dbContext;
            _messageSender = messageSender;
        }

        public async Task HandleAsync(FinishParseCianEvent @event)
        {
            var users = await _dbContext.Users
                .Include(x => x.NotificationContext)
                .Include(x => x.UserContext)
                .Where(x => x.NotificationContext.IsActive == true && x.NotificationContext.NotificationType == Entities.Enums.NotificationType.Default)
                .ToListAsync();

            foreach (var user in users)
            {
                var flats = await _dbContext.Flats
                    .Where(x => x.Price <= user.UserContext.MaximumPrice
                            && x.Price >= user.UserContext.MinimumPrice
                            && x.TimeToMetro <= user.UserContext.TimeToMetro
                            && !user.UserContext.Notifications.Value.Contains(x.CianId))
                    .AsNoTracking()
                    .Take(70)
                    .ToListAsync();

                if (!flats.Any())
                    continue;

                int elementsInChunk = 15;

                var chunks = (int)Math.Round((double)flats.Count / elementsInChunk);

                if (chunks == 0)
                    chunks = 1;

                for (int i = 0; i < chunks; i++)
                {
                    var flatsToSend = flats.Skip(i * elementsInChunk).Take(elementsInChunk);
                    var builder = new StringBuilder();

                    foreach (var item in flatsToSend)
                    {
                        var pledge = item.Pledge == null ? "Нет" : item.Pledge.ToString();
                        var comission = item.Comission == null ? "Нет" : item.Comission.ToString();

                        builder.AppendLine("Новая квартира")
                            .AppendLine($"Цена {item.Price}")
                            .AppendLine($"Залог {pledge}")
                            .AppendLine($"Комиссия {comission}%")
                            .AppendLine($"Метро: {item.Metro}")
                            .AppendLine($"Адрес:{item.Address}")
                            .AppendLine($"Этаж: {item.CurrentFloor}")
                            .AppendLine($"Этажей в доме: {item.MaxFloor}")
                            .AppendLine($"Ссылка: {item.CianReference}")
                            .AppendLine(string.Empty);

                        user.UserContext.Notifications.Value.Add(item.CianId);
                    }

                    await _messageSender.SendMessageAsync(builder.ToString(), user.ChatId);
                }

                user.NotificationContext.ChangeLastNotifyDate(DateTime.Now);
                user.UserContext.UpdatePostedNotifications();

                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
