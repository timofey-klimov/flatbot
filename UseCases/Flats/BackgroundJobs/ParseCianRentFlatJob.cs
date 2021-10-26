using Infrastructure.Interfaces.Bus;
using Infrastructure.Interfaces.Cian;
using Infrastructure.Interfaces.DataAccess;
using Infrastructure.Interfaces.Jobs;
using Infrastructure.Interfaces.Logger;
using System.Threading;
using System.Threading.Tasks;

namespace UseCases.Flats.BackgroundJobs
{
    public class ParseCianRentFlatJob : CianJob, IJob  
    {
        private ICianMapManager _cianMapManager;
        public ParseCianRentFlatJob(
            IParseCianHtmlManager parseManager,
            ICianMapManager cianMapManager,
            ILoggerService logger,
            ICianUrlBuilder urlBuilder,
            IFinderCianFlatsByHtml finderCianFlatsByHtml,
            ICianFlatsCreator cianFlatsCreator)
          
            : base(parseManager, logger, urlBuilder, finderCianFlatsByHtml, cianFlatsCreator)
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
