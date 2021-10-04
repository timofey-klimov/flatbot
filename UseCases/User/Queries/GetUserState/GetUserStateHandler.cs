using AutoMapper;
using Infrastructure.Interfaces.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using UseCases.User.Dto;

namespace UseCases.User.Queries.GetUserState
{
    public class GetUserStateHandler : IRequestHandler<GetUserStateRequest, UserStates>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetUserStateHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<UserStates> Handle(GetUserStateRequest request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .Include(x => x.UserContext)
                .ThenInclude(x => x.State)
                .FirstOrDefaultAsync(x => x.ChatId == request.ChatId);

            return _mapper.Map<UserStates>(user.UserContext.State.State);
        }
    }
}
