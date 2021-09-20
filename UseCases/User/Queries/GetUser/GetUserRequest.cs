using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.User.Base;
using UseCases.User.Queries.Dto;

namespace UseCases.User.Queries.GetUser
{
    public class GetUserRequest : BaseUserRequest<UserDto>
    {
        public GetUserRequest(long chatId)
            : base(chatId)
        {

        }
    }
}
