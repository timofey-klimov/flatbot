using Infrastructure.Interfaces.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace UseCases.User.Commands.CreateUser
{
    public class CreateUserRequestHandler : IRequestHandler<CreateUserRequest>
    {
        private IDbContext _dbContext;
        public CreateUserRequestHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.ChatId == request.ChatId);

            if (user == null)
            {
                var createUser = new Entities.Models.User(request.ChatId, request.UserName);

                await _dbContext.Users.AddAsync(createUser);
                await _dbContext.SaveChangesAsync();
            }

            return Unit.Value;
        }
    }
}
