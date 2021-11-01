using Infrastructure.Interfaces.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Common.Exceptions;

namespace UseCases.User.Commands.UpdateFlatArea
{
    public class UpdateFlatAreaHandler : IRequestHandler<UpdateFlatAreaqRequest>
    {
        private readonly IDbContext _dbContext;
        public UpdateFlatAreaHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(UpdateFlatAreaqRequest request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .Include(x => x.UserContext)
                .FirstOrDefaultAsync(x => x.ChatId == request.ChatId);

            if (user == null)
                throw new UserNotFoundException(request.ChatId);

            user.UserContext.UpdateFlatArea(request.FlatArea);

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
