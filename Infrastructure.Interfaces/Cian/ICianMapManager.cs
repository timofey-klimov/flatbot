using Infrastructure.Interfaces.Cian.Dto;
using Infrastructure.Interfaces.Cian.Enums;
using System.Collections.Generic;

namespace Infrastructure.Interfaces.Cian
{
    public interface ICianMapManager
    {
        ICollection<MapInfo> GetMapsInfo();

        IEnumerable<City> GetCities();
    }
}
