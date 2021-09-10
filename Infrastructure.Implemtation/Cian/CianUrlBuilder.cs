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

        public string BuildCianUrl(City city, OperationType type, int page)
        {
            var map = _cianMapManager.GetMapsInfo().FirstOrDefault(x => x.City == city);

            if (map == null)
                throw new MapNotFoundException($"Map with city {nameof(city)} not found");

            var stringBuilder = new StringBuilder(map.BaseUrl);

            switch (type)
            {
                case OperationType.GetExcel:
                    stringBuilder.Append("export/xls/offers/");
                    break;
                case OperationType.GetFlats:
                    stringBuilder.Append("cat.php");
                    break;
            }

            stringBuilder.Append("?deal_type=rent");
            stringBuilder.Append("&type=4");

            stringBuilder.Append("&engine_version=2");
            stringBuilder.Append("&offer_type=flat");
            stringBuilder.Append($"&region={map.Region}");
            stringBuilder.Append($"&p={page}");
            stringBuilder.Append("&room1=1");
            stringBuilder.Append("&room2=1");


            return stringBuilder.ToString();
        }
    }
}
