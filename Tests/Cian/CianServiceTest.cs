using Infrastructure.Implemtation.Cian;
using Infrastructure.Implemtation.Cian.HttpClient;
using Infrastructure.Implemtation.Logger;
using Infrastructure.Interfaces.Cian;
using Infrastructure.Interfaces.Cian.Dto;
using Infrastructure.Interfaces.Cian.Enums;
using NUnit.Framework;
using System;
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

            _cian = new CianService(new CianUrlBuilder(new CianMapManager(mapInfo)), 
                new CianHttpClient(
                    new LoggerService(
                        (s) => Console.WriteLine(s), 
                        (s) => Console.WriteLine(s), 
                        (s) => Console.WriteLine(s))
                    ));
        }

        [Test]
        public async Task GetPagesCount()
        {
            var count = await _cian.GetPagesCount(City.Moscow);
        }
    }
}
