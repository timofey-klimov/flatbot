using UseCases.User.Base;

namespace UseCases.User.Commands.UpdateBuildingYear
{
    public class UpdateBuildingYearRequest : BaseUserRequest
    {
        public int BuildingYear { get; }
        public UpdateBuildingYearRequest(long chatId, int buildingYear) 
            : base(chatId)
        {
            BuildingYear = buildingYear;
        }
    }
}
