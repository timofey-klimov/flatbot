using Infrastructure.Implemtation.Cian.Dto;
using Infrastructure.Implemtation.Cian.Exceptions;
using Infrastructure.Interfaces.Cian;
using Infrastructure.Interfaces.Cian.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utils;

namespace Infrastructure.Implemtation.Cian
{
    public class CianUrlBuilder : ICianUrlBuilder
    {
        private IEnumerable<MapInfo> _mapInfo;
        public CianUrlBuilder(IEnumerable<MapInfo> mapInfo)
        {
            _mapInfo = mapInfo;
        }

        public string BuildCianUrl(City city, DealType dealType, Room room, OperationType type, int page)
        {
            var map = _mapInfo.FirstOrDefault(x => x.City == city);

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

            switch (dealType)
            {
                case DealType.Rent:
                    stringBuilder.Append("?deal_type=rent");
                    stringBuilder.Append("&type=4");
                    break;
                case DealType.Sale:
                    stringBuilder.Append("?deal_type=sale");
                    break;
            }

            stringBuilder.Append("&engine_version=2");
            stringBuilder.Append("&offer_type=flat");
            stringBuilder.Append($"&region={map.Region}");
            stringBuilder.Append($"&p={page}");

            switch (room.To<int>())
            {
                case 1:
                    stringBuilder.Append("&room1=1");
                    break;
                case 2:
                    stringBuilder.Append("&room2=1");
                    break;
                case 3:
                    stringBuilder.Append("&room1=1");
                    stringBuilder.Append("&room2=1");
                    break;
            }

            return stringBuilder.ToString();
        }
    }
}
