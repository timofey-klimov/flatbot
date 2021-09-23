using UseCases.User.Base;

namespace UseCases.Distincts.Commands.UpdateUsersDistincts
{
    public class UpdateUsersDistrictsRequest : BaseUserRequest
    {
        public string DistrictName { get; }

        public UpdateUsersDistrictsRequest(long chatId, string distinctName)
            : base(chatId)
        {
            DistrictName = distinctName;
        }
    }
}
