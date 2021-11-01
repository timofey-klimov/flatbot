using Newtonsoft.Json;

namespace UseCases.User.Dto
{
    public class UserRoomsCountMenuDto
    {
        [JsonProperty("roomsCount")]
        public int RoomsCount { get; }

        [JsonProperty("displayName")]
        public string DisplayName { get; }

        [JsonProperty("isSelected")]
        public bool IsSelected { get; }

        public UserRoomsCountMenuDto(int roomsCount, string name, bool isSelected)
        {
            RoomsCount = roomsCount;
            IsSelected = isSelected;
            DisplayName = name;
        }
    }
}
