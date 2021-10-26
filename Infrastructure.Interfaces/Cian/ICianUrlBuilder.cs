using Infrastructure.Interfaces.Cian.Enums;

namespace Infrastructure.Interfaces.Cian
{
    public interface ICianUrlBuilder
    {
        string BuildCianUrlByPage(City city, int page);

        string BuildCianUrlByTimeInterval(City city, int timeInterval);
    }
}
