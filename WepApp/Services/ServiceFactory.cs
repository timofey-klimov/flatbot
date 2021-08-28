using Infrastructure.Implemtation.Cian;
using Infrastructure.Implemtation.Cian.Dto;
using Infrastructure.Implemtation.Logger;
using Infrastructure.Interfaces.Cian.Enums;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using Utils;
using WepApp.Exceptions;
using WepApp.Settings;

namespace WepApp.Services
{
    public class ServiceFactory
    {
        public CianUrlBuilder CreateCianUlrBuilder(IConfiguration configuration, IServiceProvider provider)
        {
            try
            {
                var maps = configuration
                    .GetSection("CianUrl")
                    .Get<IEnumerable<Map>>();

                var list = new List<MapInfo>();

                foreach (var map in maps)
                {
                    var city = Enum.Parse(typeof(City), map.City).To<City>();
                    list.Add(new MapInfo() { BaseUrl = map.Url, City = city, Region = map.Region });
                }

                return new CianUrlBuilder(list);
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
                    .Console()
                    .WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day)
                    .CreateLogger();

                return new LoggerService(logger.Error, logger.Information);
            }
            catch (Exception ex)
            {
                throw new CannotCreateServiceException(typeof(LoggerService));
            }
        }
    }
}
