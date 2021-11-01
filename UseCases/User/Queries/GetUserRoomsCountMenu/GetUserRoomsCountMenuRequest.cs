using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.User.Base;
using UseCases.User.Dto;

namespace UseCases.User.Queries.GetUserRoomsCountMenu
{
    public class GetUserRoomsCountMenuRequest : BaseUserRequest<ICollection<UserRoomsCountMenuDto>>
    {
        public GetUserRoomsCountMenuRequest(long chatId) : base(chatId)
        {
        }
    }
}
