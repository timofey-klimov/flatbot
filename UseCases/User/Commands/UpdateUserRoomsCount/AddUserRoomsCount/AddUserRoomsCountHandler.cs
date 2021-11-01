using Infrastructure.Interfaces.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Common.Exceptions;

namespace UseCases.User.Commands.UpdateUserRoomsCount.AddUserRoomsCount
{
    public class AddUserRoomsCountHandler : IRequestHandler<AddUserRoomsCountRequest>
    {
        private readonly IDbContext _dbContext;
        public AddUserRoomsCountHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(AddUserRoomsCountRequest request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .Include(x => x.UserContext)
                .ThenInclude(x => x.UserRoomCounts)
                .FirstOrDefaultAsync(x => x.ChatId == request.ChatId);

            if (user == null)
                throw new UserNotFoundException(request.ChatId);

            user.UserContext.AddRoomsCount(request.RoomsCount);

            await _dbContext.SaveChangesAsync();

            return Unit.Value;

        }
    }
}
