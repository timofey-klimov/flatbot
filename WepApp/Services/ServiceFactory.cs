using Infrastructure.Implemtation.Cian;
using Infrastructure.Implemtation.Logger;
using Infrastructure.Interfaces.Cian.Dto;
using Infrastructure.Interfaces.Cian.Enums;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using Utils;
using WepApp.Exceptions;
using WepApp.Settings;

namespace WepApp.Services
{
    public class ServiceFactory
    {
        public CianMapManager CreateMapManager(IConfiguration configuration, IServiceProvider provider)
        {
            try
            {
                var maps = configuration
                    .GetSection("CianUrl")
                    .GetSection("Maps")
                    .Get<IEnumerable<Map>>();

                var list = new List<MapInfo>();

                foreach (var map in maps)
                {
                    var city = Enum.Parse(typeof(City), map.City).To<City>();
                    list.Add(new MapInfo() { BaseUrl = map.Url, City = city, Region = map.Region });
                }

                return new CianMapManager(list);
            }
            catch (Exception ex)
            {
                throw new CannotCreateServiceException(typeof(CianUrlBuilder));
            }
        }

        public LoggerService CreateLogger(IServiceProvider provider)
        {
            try
            {
                var logger = new LoggerConfiguration()
                    .WriteTo
                    .Console(restrictedToMinimumLevel: LogEventLevel.Information)
                    .CreateLogger();

                return new LoggerService(logger.Error, logger.Information, logger.Debug);
            }
            catch (Exception ex)
            {
                throw new CannotCreateServiceException(typeof(LoggerService));
            }
        }
    }
}
