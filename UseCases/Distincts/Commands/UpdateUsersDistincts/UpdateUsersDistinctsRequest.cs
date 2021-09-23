using UseCases.User.Base;

namespace UseCases.Distincts.Commands.UpdateUsersDistincts
{
    public class UpdateUsersDistinctsRequest : BaseUserRequest
    {
        public string DistinctName { get; }

        public UpdateUsersDistinctsRequest(long chatId, string distinctName)
            : base(chatId)
        {
            DistinctName = distinctName;
        }
    }
}
