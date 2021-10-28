using Infrastructure.Implemtation.Telegram.Factory;
using Infrastructure.Interfaces.Bus;
using Infrastructure.Interfaces.Common.Events;
using Infrastructure.Interfaces.DataAccess;
using Infrastructure.Interfaces.Telegram;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implemtation.Common.EventHandlers
{
    public class NewFlatCreatedHandler : IEventBusHandler<NewFlatCreatedEvent>
    {
        private readonly IDbContext _dbContext;
        private readonly INotificationCreatorFactory _notificationFactory;
        private readonly ITelegramMessageSender _messageSender;

        public NewFlatCreatedHandler(
            IDbContext dbContext,
            INotificationCreatorFactory factory,
            ITelegramMessageSender messageSender)
        {
            _dbContext = dbContext;
            _notificationFactory = factory;
            _messageSender = messageSender;
        }

        public async Task HandleAsync(NewFlatCreatedEvent @event)
        {
            var users = _dbContext.Users
                .FromSqlRaw($"exec FindUsersForMessaging @flatId = {@event.Id}")
                .ToArray();
          
            if (!users.Any())
                return;

            var flat = await _dbContext
                .Flats
                .Include(x => x.Address)
                .Include(x => x.BuildingInfo)
                .Include(x => x.PriceInfo)
                .Include(x => x.UndergroundInfos)
                .FirstOrDefaultAsync(x => x.Id == @event.Id);

            foreach (var user in users)
            {
                var notifyCreator = _notificationFactory.Create(Interfaces.Telegram.Model.NotificationCreationType.Default);

                var notification = await notifyCreator.CreateAsync(flat, user.ChatId);

                await _messageSender.SendMessagesAsync(new[] { notification }, user.ChatId);
            }
        }
    }
}
