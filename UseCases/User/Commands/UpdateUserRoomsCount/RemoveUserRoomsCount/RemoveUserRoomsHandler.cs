using Infrastructure.Interfaces.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Common.Exceptions;

namespace UseCases.User.Commands.UpdateUserRoomsCount.RemoveUserRoomsCount
{
    public class RemoveUserRoomsHandler : IRequestHandler<RemoveUserRoomsCountRequest>
    {
        private readonly IDbContext _dbContext;
        public RemoveUserRoomsHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(RemoveUserRoomsCountRequest request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .Include(x => x.UserContext)
                    .ThenInclude(x => x.UserRoomCounts)
                .FirstOrDefaultAsync(x => x.ChatId == request.ChatId);

            if (user == null)
                throw new UserNotFoundException(request.ChatId);

            user.UserContext.RemoveRoomsCount(request.RoomsCount);

            await _dbContext.SaveChangesAsync();

            return Unit.Value;

        }
    }
}
