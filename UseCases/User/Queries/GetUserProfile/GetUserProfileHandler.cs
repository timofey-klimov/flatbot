using Entities.Enums;
using Infrastructure.Interfaces.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UseCases.User.Exceptions;

namespace UseCases.User.Queries.GetUserProfile
{
    public class GetUserProfileHandler : IRequestHandler<GetUserProfileRequest, string>
    {
        private IDbContext _dbContext;

        public GetUserProfileHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> Handle(GetUserProfileRequest request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .Include(x => x.UserContext)
                .Include(x => x.NotificationContext)
                .FirstOrDefaultAsync(x => x.ChatId == request.ChatId);

            if (user == null)
                throw new UserIsNullException("No such user");

            var userContext = user.UserContext;

            var stringBuilder = new StringBuilder();

            var isActive = user.NotificationContext.IsActive ? "Да" : "Нет";

            var type = user.NotificationContext.NotificationType switch
            {
                NotificationType.Default => "По обнаружению новых",
                NotificationType.EveryDay => "Раз в день",
                NotificationType.EveryWeek => "Раз в неделю"
            };


            stringBuilder
                .AppendLine($"Твой профиль:")
                .AppendLine("--Настройки поиска квартир--")
                .AppendLine($"Минимальная цена: {userContext.MinimumPrice}")
                .AppendLine($"Максимальная цена: {userContext.MaximumPrice}")
                .AppendLine($"Время до метро: {userContext.TimeToMetro}")
                .AppendLine("--Настройки уведомлений--")
                .AppendLine($"Присылать уведомления: {isActive}")
                .AppendLine($"Частота уведомлений: {type}");
            

            return stringBuilder.ToString();
        }
    }
}
