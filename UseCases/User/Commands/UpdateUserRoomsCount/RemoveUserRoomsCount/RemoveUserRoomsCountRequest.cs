using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.User.Base;

namespace UseCases.User.Commands.UpdateUserRoomsCount.RemoveUserRoomsCount
{
    public class RemoveUserRoomsCountRequest : BaseUserRequest
    {
        public int RoomsCount { get; }

        public RemoveUserRoomsCountRequest(long chatId, int rooomsCount) 
            : base(chatId)
        {
            RoomsCount = rooomsCount;
        }
    }
}
