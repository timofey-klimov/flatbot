using UseCases.User.Base;
using UseCases.User.Dto;

namespace UseCases.User.Commands.ChangeUserState
{
    public class ChangeUserStateRequest : BaseUserRequest
    {
        public UserStates UserState { get; }
        public ChangeUserStateRequest(long chatId, UserStates state) : base(chatId)
        {
            UserState = state;
        }
    }
}
