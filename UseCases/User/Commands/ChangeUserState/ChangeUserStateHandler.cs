using AutoMapper;
using Infrastructure.Interfaces.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Common.Exceptions;

namespace UseCases.User.Commands.ChangeUserState
{
    public class ChangeUserStateHandler : IRequestHandler<ChangeUserStateRequest>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public ChangeUserStateHandler(
            IDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(ChangeUserStateRequest request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .Include(x => x.UserContext)
                .ThenInclude(x => x.State)
                .FirstOrDefaultAsync(x => x.ChatId == request.ChatId);

            if (user == null)
                throw new UserIsNullException(request.ChatId);

            var state = _mapper.Map<Entities.Enums.UserStates>(request.UserState);

            user.UserContext.ChangeState(state);

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
