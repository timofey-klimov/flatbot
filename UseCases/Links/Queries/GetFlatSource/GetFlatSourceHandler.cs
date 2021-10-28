using Infrastructure.Interfaces.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Common.Exceptions;
using UseCases.Links.Queries.GetFlatSource.Dto;

namespace UseCases.Links.Queries.GetFlatSource
{
    public class GetFlatSourceHandler : IRequestHandler<GetFlatSourceRequest, SourceUrlDto>
    {
        private readonly IDbContext _dbContext;
        public GetFlatSourceHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SourceUrlDto> Handle(GetFlatSourceRequest request, CancellationToken cancellationToken)
        {
            var flat = await _dbContext.Flats
                .FirstOrDefaultAsync(x => x.Id == request.FlatId);

            if (flat == null)
                throw new FlatNotFoundExeption(request.FlatId);

            var user = await _dbContext.Users
                .Include(x => x.FollowedLinks)
                .FirstOrDefaultAsync(x => x.ChatId == request.ChatId);

            if (user == null)
                throw new UserNotFoundException(request.ChatId);

            user.UpdateFollowedLinks(flat.CianUrl);

            await _dbContext.SaveChangesAsync();

            return new SourceUrlDto(flat.CianUrl);
        }
    }
}
