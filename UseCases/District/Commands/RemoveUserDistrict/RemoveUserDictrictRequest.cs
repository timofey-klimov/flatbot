using UseCases.User.Base;

namespace UseCases.District.Commands.RemoveUserDistrict
{
    public class RemoveUserDictrictRequest : BaseUserRequest
    {
        public string DistrictName { get; }
        public RemoveUserDictrictRequest(long chatId, string districtName)
            : base(chatId)
        {
            DistrictName = districtName;
        }
    }
}
