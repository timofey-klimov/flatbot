using UseCases.User.Base;

namespace UseCases.Distincts.Commands.UpdateUsersDistincts
{
    public class AddUserDistrictRequest : BaseUserRequest
    {
        public string DistrictName { get; }

        public AddUserDistrictRequest(long chatId, string distinctName)
            : base(chatId)
        {
            DistrictName = distinctName;
        }
    }
}
