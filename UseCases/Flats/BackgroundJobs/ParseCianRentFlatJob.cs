using Infrastructure.Interfaces.Bus;
using Infrastructure.Interfaces.Cian;
using Infrastructure.Interfaces.Cian.Enums;
using Infrastructure.Interfaces.Cian.HttpClient;
using Infrastructure.Interfaces.Jobs;
using Infrastructure.Interfaces.Logger;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace UseCases.Flats.BackgroundJobs
{
    public class ParseCianRentFlatJob : CianJob, IJob  
    {
        private ICianMapManager _cianMapManager;
        public ParseCianRentFlatJob(
            IParseCianManager parseManager,
            ICianMapManager cianMapManager,
            ILoggerService logger,
            IEventBus eventBus,
            ICianUrlBuilder urlBuilder)
            : base(parseManager, logger, eventBus, urlBuilder)
        {
            _cianMapManager = cianMapManager;
        }

        public async Task ExecuteAsync(CancellationToken token)
        {
            var cities = _cianMapManager.GetCities();

            foreach (var city in cities)
            {
                 await Execute(city, token);
            }
        }
    }
}
