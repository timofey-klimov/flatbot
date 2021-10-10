using Infrastructure.Implemtation.Cian.Exceptions;
using Infrastructure.Interfaces.Cian;
using Infrastructure.Interfaces.Cian.Enums;
using System.Linq;
using System.Text;

namespace Infrastructure.Implemtation.Cian
{
    public class CianUrlBuilder : ICianUrlBuilder
    {
        private ICianMapManager _cianMapManager;
        public CianUrlBuilder(
            ICianMapManager cianMapManager)
        {
            _cianMapManager = cianMapManager;
        }

        public string BuildCianUrl(City city, int page)
        {
            var map = _cianMapManager.GetMapsInfo().FirstOrDefault(x => x.City == city);

            if (map == null)
                throw new MapNotFoundException($"Map with city {nameof(city)} not found");

            var stringBuilder = new StringBuilder(map.BaseUrl);

            stringBuilder.Append("cat.php");

            stringBuilder.Append("?deal_type=rent");
            stringBuilder.Append("&type=4");

            stringBuilder.Append("&engine_version=2");
            stringBuilder.Append("&maxprice=45000");
            stringBuilder.Append("&offer_type=flat");
            stringBuilder.Append($"&region={map.Region}");
            stringBuilder.Append($"&p={page}");
            stringBuilder.Append("&room1=1");

            return stringBuilder.ToString();
        }
    }
}
