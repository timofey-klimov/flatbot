using Infrastructure.Interfaces.Cian.Enums;

namespace Infrastructure.Interfaces.Cian
{
    public interface ICianUrlBuilder
    {
        string BuildCianUrl(City city, OperationType type, int page);
    }
}
