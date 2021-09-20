using AutoMapper;
using Entities.Enums;
using Infrastructure.Interfaces.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using UseCases.User.Exceptions;

namespace UseCases.Notifications.Commands.SelectNotificationType
{
    public class SelectNotificationTypeHandler : IRequestHandler<SelectNotificationTypeRequest>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public SelectNotificationTypeHandler(
            IDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(SelectNotificationTypeRequest request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .Include(x => x.NotificationContext)
                .FirstOrDefaultAsync(x => x.ChatId == request.ChatId);

            if (user == null)
                throw new UserIsNullException("No such user");

            var notType = _mapper.Map<NotificationType>(request.NotificationType);

            user.ChangeNotificationType(notType);

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
