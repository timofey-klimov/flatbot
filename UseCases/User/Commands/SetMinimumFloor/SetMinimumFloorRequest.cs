using UseCases.User.Base;

namespace UseCases.User.Commands.SetMinimumFloor
{
    public class SetMinimumFloorRequest : BaseUserRequest
    {
        public int MinimumFloor { get; }

        public SetMinimumFloorRequest(long chatId, int minimumFloor)
                 : base(chatId)
        {
                MinimumFloor = minimumFloor;
        }
    }
}
