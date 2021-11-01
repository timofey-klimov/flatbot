using UseCases.User.Base;

namespace UseCases.User.Commands.UpdateUserRoomsCount.AddUserRoomsCount
{
    public class AddUserRoomsCountRequest : BaseUserRequest
    {
        public int RoomsCount { get; }

        public AddUserRoomsCountRequest(long chatId, int roomsCount) 
            : base(chatId)
        {
            RoomsCount = roomsCount;
        }
    }
}
