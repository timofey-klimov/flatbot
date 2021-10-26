using Entities.Models.FlatEntities;
using Infrastructure.Implemtation.Cian.Exceptions;
using Infrastructure.Interfaces.Cian;
using Infrastructure.Interfaces.Cian.Dto;
using Infrastructure.Interfaces.DataAccess;
using Infrastructure.Interfaces.Logger;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implemtation.Cian
{
    public class CianFlatJsonParser : ICianFlatJsonParser
    {
        private readonly IDbContext _dbContext;
        private readonly ILoggerService _logger;
        public CianFlatJsonParser(
            IDbContext dbContext,
            ILoggerService logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<int> ParseAsync(JToken token, FindedFlatDto dto)
        {

            var flat = new Flat(dto.CianId, dto.CianUrl);

            var offerData = token["offerData"]["offer"];

            var building = offerData["building"];

            var buildYear = building.Value<int?>("buildYear");

            var materialType = building.Value<string>("materialType")
                ?? building.Value<string>("houseMaterialType");

            flat.UpdateBuildingInfo(buildYear, materialType);

            var floorsCount = building.Value<int>("floorsCount");

            var cellingHeight = building.Value<double?>("ceilingHeight");

            var totalArea = building.Value<double?>("totalArea");

            var roomsCount = offerData.Value<int>("roomsCount");

            var floorNumber = offerData.Value<int>("floorNumber");

            var hasFurniture = offerData.Value<bool?>("hasFurniture");

            var description = offerData.Value<string>("description");
          
            var parking = building["parking"];

            if (parking.HasValues)
            {

                var parkingType = parking.Value<string>("type");

                var isFreeParking = parking.Value<bool?>("isFree");

                var parkingPriceMonthly = parking.Value<int?>("priceMonthly");

                flat.UpdateParkingInfo(parkingType, isFreeParking, parkingPriceMonthly);
            }

            var geo = offerData["geo"]["coordinates"];

            var lat = geo.Value<double>("lat");

            var lng = geo.Value<double>("lng");

            flat.UpdateFlatGeo(lat, lng);

            var addressInfo = offerData["geo"]["address"];

            var city = GetAddressPart(addressInfo, "location");

            var okrug = GetAddressPart(addressInfo, "okrug");

            var raion = GetAddressPart(addressInfo, "raion");

            var street = GetAddressPart(addressInfo, "street");

            var house = GetAddressPart(addressInfo, "house");

            var district = await _dbContext.Districts.FirstOrDefaultAsync(x => x.Name == okrug);

            flat.UpdateAddress(city, district, raion, street, house);

            var railWaysInfo = offerData["geo"]["railways"];

            foreach (var railway in railWaysInfo.Children())
            {
                var time = railway.Value<int>("time");
                var type = railway.Value<string>("travelType");
                var name = railway.Value<string>("name");

                flat.UpdateRailWayyInfo(name, time, type);
            }

            var undergroundInfo = offerData["geo"]["undergrounds"];

            foreach (var underground in undergroundInfo.Children())
            {
                var time = underground.Value<int>("travelTime");
                var type = underground.Value<string>("travelType");
                var name = underground.Value<string>("name");

                flat.UpdateUndergroundsInfo(name, time, type);
            }

            var photosInfo = offerData["photos"];

            foreach (var photoInfo in photosInfo)
            {
                var url = photoInfo.Value<string>("thumbnailUrl");

                flat.UpdateFlatsPhotos(url);
            }

            var payInfo = offerData["bargainTerms"];

            var leaseTermType = payInfo.Value<string>("leaseTermType");

            var price = payInfo.Value<int>("price");

            var agentFee = payInfo.Value<int?>("agentFee");

            var deposit = payInfo.Value<int?>("deposit");

            flat.UpdatePriceInfo(price, agentFee, deposit);

            var phonesInfo = offerData["phones"];

            foreach (var phone in phonesInfo.Children())
            {
                var code = phone.Value<string>("countryCode");
                var number = phone.Value<string>("number");

                var phoneNumber = code + number;

                flat.UpdatePhones(phoneNumber);
            }

            flat.CreateBasicParameters(
              totalArea,
              cellingHeight,
              floorsCount,
              floorNumber,
              roomsCount,
              hasFurniture,
              description,
              leaseTermType);

            _dbContext.Flats.Add(flat);

            await _dbContext.SaveChangesAsync();

            _logger.Info(this.GetType(), $"Create flat {flat.Id}");

            return flat.Id;
        }

        private string GetAddressPart(JToken addressInfo, string type)
        {
            var dictionary = new Dictionary<string, int>()
            {
                { "location",0 },
                { "okrug", 1 },
                { "raion", 2 },
                { "street", 3},
                { "house", 4 }
            };
            return addressInfo.
                   Children().ElementAt(dictionary[type])
                   .Value<string>("name");
        }

    }
}
