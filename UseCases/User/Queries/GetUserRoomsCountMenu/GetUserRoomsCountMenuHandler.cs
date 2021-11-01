using Entities.Models.UserAgregate;
using Infrastructure.Interfaces.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Common.Exceptions;
using UseCases.User.Dto;

namespace UseCases.User.Queries.GetUserRoomsCountMenu
{
    public class GetUserRoomsCountMenuHandler : IRequestHandler<GetUserRoomsCountMenuRequest, ICollection<UserRoomsCountMenuDto>>
    {
        private readonly IDbContext _dbContext;
        public GetUserRoomsCountMenuHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ICollection<UserRoomsCountMenuDto>> Handle(GetUserRoomsCountMenuRequest request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .Include(x => x.UserContext)
                    .ThenInclude(x => x.UserRoomCounts)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ChatId == request.ChatId);

            if (user == null)
                throw new UserNotFoundException(request.ChatId);

            var items = new List<UserRoomsCountMenuDto>();
            var existingRoomsCount = new List<int>() { 1, 2, 3 };

            foreach (var usersRoom in user.UserContext.UserRoomCounts)
            {
                CreateUserRoomsMenuItem(items, usersRoom.RoomCount, true);
            }

            var notSelectedRooms = from d in existingRoomsCount
                                   join u in user.UserContext.UserRoomCounts
                                   on d equals u.RoomCount into ps
                                   from p in ps.DefaultIfEmpty()
                                   where p == null
                                   select d;

            foreach (var notSelectedRoom in notSelectedRooms)
            {
                CreateUserRoomsMenuItem(items, notSelectedRoom, false);
            }

            return items.OrderBy(x => x.RoomsCount).ToList();
        }

        private void CreateUserRoomsMenuItem(List<UserRoomsCountMenuDto> items, int usersRoom, bool isSelected)
        {
            switch (usersRoom)
            {
                case 1:
                    items.Add(new UserRoomsCountMenuDto(1, "1-комн.", isSelected));
                    break;
                case 2:
                    items.Add(new UserRoomsCountMenuDto(2, "2-комн.", isSelected));
                    break;
                case 3:
                    items.Add(new UserRoomsCountMenuDto(3, "3-комн.", isSelected));
                    break;
            }
        }
    }
}
