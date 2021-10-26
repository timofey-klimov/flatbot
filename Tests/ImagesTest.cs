using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestFixture]
    public class ImagesTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Test()
        {
            using (var fileStream = new FileStream(@"C:\Users\Тимофей\Desktop\testHtml.txt", FileMode.Open))
            {
                using var streamReader = new StreamReader(fileStream);

                var text = streamReader.ReadToEnd();
                var firstChar = text.IndexOf("[{");

                text = text.Remove(0, firstChar - 1).Trim();
                text = text.Remove(text.Length - 1);

                var root = JArray.Parse(text)
                    .Children<JObject>()
                    .Where(x => x.Value<string>("key") == "defaultState")
                    .FirstOrDefault()["value"];

                var offerData = root["offerData"]["offer"];

                var building = offerData["building"];

                var buildYear = building.Value<string>("buildYear");

                var materialType = building.Value<string>("materialType") 
                    ?? building.Value<string>("houseMaterialType");

                var floorsCount = building.Value<string>("floorsCount");

                var cellingHeight = building.Value<string>("ceilingHeight");

                var totalArea = building.Value<decimal>("totalArea");

                var parking = building["parking"];

                var parkingType = parking.Value<string>("type");

                var isFreeParking = parking.Value<string>("isFree");

                var parkingPriceMonthly = parking.Value<string>("priceMonthly");


                var geo = offerData["geo"]["coordinates"];

                var lat = geo.Value<string>("lat");

                var lng = geo.Value<string>("lng");

                var addressInfo = offerData["geo"]["address"];


                var city = addressInfo.
                    Children().
                    Where(x => x.Value<string>("type") == "location")
                    .FirstOrDefault()
                    .Value<string>("name");

                var okrug = addressInfo
                    .Children()
                    .Where(x => x.Value<string>("type") == "okrug")
                    .FirstOrDefault()
                    .Value<string>("name");

                var raion = addressInfo
                    .Children()
                    .Where(x => x.Value<string>("type") == "raion")
                    .FirstOrDefault()
                    .Value<string>("name");

                var street = addressInfo
                    .Children()
                    .Where(x => x.Value<string>("type") == "street")
                    .FirstOrDefault()
                    .Value<string>("name");

                var house = addressInfo
                    .Children()
                    .Where(x => x.Value<string>("type") == "house")
                    .FirstOrDefault()
                    .Value<string>("name");

                var railWaysInfo = offerData["geo"]["railways"];

                var railwayCollection = new List<(string type, int time, string name)>();

                foreach (var railway in railWaysInfo.Children())
                {
                    var time = railway.Value<int>("time");

                    var type = railway.Value<string>("travelType");

                    var name = railway.Value<string>("name");

                    railwayCollection.Add((type, time, name));
                }

                var undergroundCollection = new List<(string type, int time, string Name)>();

                var undergroundInfo = offerData["geo"]["undergrounds"];

                foreach (var underground in undergroundInfo.Children())
                {
                    var time = underground.Value<int>("travelTime");

                    var travelType = underground.Value<string>("travelType");

                    var name = underground.Value<string>("name");

                    undergroundCollection.Add((travelType, time, name));
                }

                var hasFurniture = offerData.Value<bool>("hasFurniture");

                var floorNumber = offerData.Value<int>("floorNumber");

                var photosCollection = new List<string>();

                var photosInfo = offerData["photos"];

                foreach (var photoSource in photosInfo.Children())
                {
                    var url = photoSource.Value<string>("thumbnailUrl");

                    photosCollection.Add(url);
                }

                var payInfo = offerData["bargainTerms"];

                var leaseTermType = payInfo.Value<string>("leaseTermType");

                var price = payInfo.Value<int>("price");

                var agentFee = payInfo.Value<int>("agentFee");

                var deposit = payInfo.Value<int>("deposit");
            }
        }
    }
}
