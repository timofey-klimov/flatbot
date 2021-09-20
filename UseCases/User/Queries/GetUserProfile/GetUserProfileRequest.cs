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
