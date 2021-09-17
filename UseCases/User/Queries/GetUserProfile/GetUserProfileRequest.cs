using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.User.Base;

namespace UseCases.User.Queries.GetUserProfile
{
    public class GetUserProfileRequest : BaseUserRequest<string>
    {
        public GetUserProfileRequest(long chatId)
            : base(chatId)
        {

        }
    }
}
