using Infrastructure.Implemtation.Cian;
using Infrastructure.Implemtation.Cian.Dto;
using Infrastructure.Interfaces.Cian.Enums;
using NUnit.Framework;
using System.Collections.Generic;

namespace Tests.Cian
{
    public class CianUrlBuilderTest
    {
        private CianUrlBuilder _builder;
        [SetUp]
        public void Setup()
        {
            var mapInfo = new List<MapInfo>()
            {
                new MapInfo
                {
                    BaseUrl = "https://cian.ru/cat.php",
                    Region = 1,
                    City = City.Moscow
                }
            };

            _builder = new CianUrlBuilder(mapInfo);
            
        }

        [Test]
        public void GenerateUrl()
        {
            var urlOneRoom = _builder
                .BuildCianUrl(City.Moscow, DealType.Sale, Room.One, 2);

            var urlSecondRoom = _builder
                .BuildCianUrl(City.Moscow, DealType.Sale, Room.Two, 2);

            var urlOneAndSecondRoom = _builder
                .BuildCianUrl(City.Moscow, DealType.Sale, Room.One | Room.Two, 2);

        }
    }
}
