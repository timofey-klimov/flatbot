using AutoMapper;
using Infrastructure.Interfaces.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using UseCases.User.Exceptions;
using UseCases.User.Queries.Dto;

namespace UseCases.User.Queries.GetUser
{
    public class GetUserHandler : IRequestHandler<GetUserRequest, UserDto>
    {
        private IDbContext _dbContext;
        private IMapper _mapper;

        public GetUserHandler(
            IDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(GetUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .Include(x => x.NotificationContext)
                .FirstOrDefaultAsync(x => x.ChatId == request.ChatId);

            if (user == null)
                throw new UserIsNullException("No such user");

            return _mapper.Map<UserDto>(user);
        }
    }
}
