using Infrastructure.Interfaces.Bus;
using Infrastructure.Interfaces.Cian;
using Infrastructure.Interfaces.Cian.Enums;
using Infrastructure.Interfaces.Cian.HttpClient;
using Infrastructure.Interfaces.Logger;
using System.Threading.Tasks;

namespace UseCases.Flats.BackgroundJobs
{
    public class ParseCianRentFlatJob : CianJob  
    {
        private ICianMapManager _cianMapManager;
        public ParseCianRentFlatJob(
            ICianService cianService,
            ICianMapManager cianMapManager,
            ILoggerService logger,
            IEventBus eventBus)
            : base(cianService, logger, eventBus)
        {
            _cianMapManager = cianMapManager;
        }

        public async Task Execute()
        {
            Logger.Info("Start ParseCianRentFlatJob");

            var cities = _cianMapManager.GetCities();

            foreach (var city in cities)
            {
                await Execute(city);
            }

            Logger.Info("Finish ParseCianRentFlatJob");
        }
    }
}
