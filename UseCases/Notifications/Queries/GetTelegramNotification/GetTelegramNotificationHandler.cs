using Infrastructure.Interfaces.DataAccess;
using Infrastructure.Interfaces.Telegram;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace UseCases.Notifications.Queries.GetTelegramNotification
{
    public class GetTelegramNotificationHandler : IRequestHandler<GetTelegramNotificationRequest, string>
    {
        private IDbContext _dbContext;
        private ITelegramNotificationService _tgNotifyService;

        public GetTelegramNotificationHandler(
            IDbContext dbContext,
            ITelegramNotificationService tgNofifyService)
        {
            _dbContext = dbContext;
            _tgNotifyService = tgNofifyService;
        }
        public async Task<string> Handle(GetTelegramNotificationRequest request, CancellationToken cancellationToken)
        {
            var user = await _dbContext
                .Users
                .Include(x => x.UserContext)
                .ThenInclude(x => x.Disctricts)
                .Include(x => x.NotificationContext)
                .FirstOrDefaultAsync(x => x.ChatId == request.ChatId);

            var flats = await _dbContext.Flats
                .Where(x => x.Price <= user.UserContext.MaximumPrice
                        && x.Price >= user.UserContext.MinimumPrice
                        && x.TimeToMetro < user.UserContext.TimeToMetro
                        && x.CurrentFloor >= user.UserContext.MinimumFloor
                        && user.UserContext.Disctricts.Contains(x.District)
                        && !user.UserContext.NotificationsList.Value.Contains(x.CianId))
                .OrderBy(x => x.Price)
                .Take(15)
                .ToListAsync(cancellationToken);

            if (!flats.Any())
            {
                return "Объявлений по твоим фильтрам не найдено";
            }

            var messages = _tgNotifyService.CreateMany(flats, flats.Count);

            user.NotificationContext.CreateLastNotifyDateNow();
            user.UserContext.AddNotifications(flats.Select(x => x.CianId));

            await _dbContext.SaveChangesAsync(cancellationToken);

            return messages.FirstOrDefault();
        }
    }
}
