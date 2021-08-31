using Infrastructure.Interfaces.Cian.Dto;
using Infrastructure.Interfaces.Cian;
using System.Collections.Generic;
using Infrastructure.Interfaces.Cian.Enums;
using System.Linq;

namespace Infrastructure.Implemtation.Cian
{
    public class CianMapManager : ICianMapManager
    {
        private ICollection<MapInfo> _maps;

        public CianMapManager(ICollection<MapInfo> maps)
        {
            _maps = maps;
        }

        public ICollection<MapInfo> GetMapsInfo() => _maps;

        public IEnumerable<City> GetCities() => _maps.Select(x => x.City);
    }
}
