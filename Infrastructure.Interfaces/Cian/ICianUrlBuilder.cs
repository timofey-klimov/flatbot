using Infrastructure.Interfaces.Cian.Enums;

namespace Infrastructure.Interfaces.Cian
{
    public interface ICianUrlBuilder
    {
        string BuildCianUrl(City city, DealType dealType, Room room, OperationType type,int page);
    }
}
