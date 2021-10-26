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

        public string BuildCianUrlByPage(City city, int page)
        {
            var template = BuildCianUrl(city);

            var stringBuilder = new StringBuilder(template);

            stringBuilder.Append($"&p={page}");

            return stringBuilder.ToString();
        }

        public string BuildCianUrlByTimeInterval(City city, int timeInterval)
        {
            var template = BuildCianUrl(city);

            var stringBuilder = new StringBuilder(template);

            stringBuilder.Append($"&totime={timeInterval}");


            return stringBuilder.ToString();
        }
        private string BuildCianUrl(City city)
        {
            var map = _cianMapManager.GetMapsInfo().FirstOrDefault(x => x.City == city);

            if (map == null)
                throw new MapNotFoundException($"Map with city {nameof(city)} not found");

            var stringBuilder = new StringBuilder(map.BaseUrl);

            stringBuilder.Append("cat.php");
            stringBuilder.Append("?deal_type=rent");
            stringBuilder.Append("&type=4");
            stringBuilder.Append("&engine_version=2");
            stringBuilder.Append("&maxprice=100000");
            stringBuilder.Append("&offer_type=flat");
            stringBuilder.Append($"&region={map.Region}");
            stringBuilder.Append("&room1=1");
            stringBuilder.Append("&room2=1");
            stringBuilder.Append("&room3=1");

            return stringBuilder.ToString();
        }

    }
}
