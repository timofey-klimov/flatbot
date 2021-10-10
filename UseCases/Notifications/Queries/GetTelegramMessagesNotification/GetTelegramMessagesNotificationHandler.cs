using Infrastructure.Interfaces.Common;
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
    public class GetTelegramMessagesNotificationHandler : IRequestHandler<GetTelegramMessagesNotificationRequest, string>
    {
        private readonly IDbContext _dbContext;
        private readonly ITelegramNotificationCreator _tgNotifyService;
        private readonly IFilterFlatService _filter;
        private readonly IFlatCountInMessageManager _flatCountManager;

        public GetTelegramMessagesNotificationHandler(
            IDbContext dbContext,
            ITelegramNotificationCreator tgNofifyService,
            IFilterFlatService filter,
            IFlatCountInMessageManager flatCountManger)
        {
            _dbContext = dbContext;
            _tgNotifyService = tgNofifyService;
            _filter = filter;
            _flatCountManager = flatCountManger;
        }
        public async Task<string> Handle(GetTelegramMessagesNotificationRequest request, CancellationToken cancellationToken)
        {
            var user = await _dbContext
                .Users
                .Include(x => x.UserContext)
                .ThenInclude(x => x.Disctricts)
                .Include(x => x.NotificationContext)
                .FirstOrDefaultAsync(x => x.ChatId == request.ChatId);

            var flats = await _filter.GetFlatsByUserContextAsync(user.UserContext, _flatCountManager.FlatCount, cancellationToken);

            if (!flats.Any())
            {
                return "Объявлений по твоим фильтрам не найдено";
            }

            var messages = _tgNotifyService.CreateMessages(flats, flats.Count);

            user.NotificationContext.CreateLastNotifyDateNow();
            user.UserContext.AddNotifications(flats.Select(x => x.CianId));

            await _dbContext.SaveChangesAsync(cancellationToken);

            return messages.FirstOrDefault();
        }
    }
}
