using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.User.Base;
using UseCases.User.Dto;

namespace UseCases.User.Queries.GetUserState
{
    public class GetUserStateRequest : BaseUserRequest<UserStates>
    {
        public GetUserStateRequest(long chatId) : base(chatId)
        {
        }
    }
}
