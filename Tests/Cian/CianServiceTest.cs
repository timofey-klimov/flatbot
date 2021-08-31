using Infrastructure.Implemtation.Cian;
using Infrastructure.Interfaces.Cian;
using Infrastructure.Interfaces.Cian.Dto;
using Infrastructure.Interfaces.Cian.Enums;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tests.Cian
{
    public class CianServiceTest
    {
        private ICianService _cian;
        [SetUp]
        public void Setup()
        {
            var mapInfo = new List<MapInfo>()
            {
                new MapInfo
                {
                    BaseUrl = "https://cian.ru/",
                    Region = 1,
                    City = City.Moscow
                }
            };

            _cian = new CianService(new CianUrlBuilder(new CianMapManager(mapInfo)),new System.Net.Http.HttpClient());
        }

        [Test]
        public async Task GetPagesCount()
        {
            var count = await _cian.GetPagesCount(City.Moscow, DealType.Rent, Room.One);
        }
    }
}
