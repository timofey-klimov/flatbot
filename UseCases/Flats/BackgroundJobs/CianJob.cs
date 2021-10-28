using Infrastructure.Interfaces.Cian;
using Infrastructure.Interfaces.Cian.Enums;
using Infrastructure.Interfaces.Logger;
using System.Threading;
using System.Threading.Tasks;

namespace UseCases.Flats.BackgroundJobs
{
    public abstract class CianJob
    {
        protected IParseCianHtmlManager ParseCianManager;
        protected ILoggerService Logger;
        protected ICianUrlBuilder UrlBuilder;
        protected IFinderCianFlatsByHtml FinderFlats;
        protected ICianFlatsCreator FlatsCreator;

        public CianJob(
            IParseCianHtmlManager cianService,
            ILoggerService logger,
            ICianUrlBuilder urlBuilder,
            IFinderCianFlatsByHtml creatorFlats,
            ICianFlatsCreator cianFlatsCreator)
        {
            ParseCianManager = cianService;
            Logger = logger;
            UrlBuilder = urlBuilder;
            FinderFlats = creatorFlats;
            FlatsCreator = cianFlatsCreator;
            
        }
        protected async Task Execute(City city, CancellationToken token)
        {
            if (token.IsCancellationRequested)
                return;

            var url = UrlBuilder.BuildCianUrlByTimeInterval(city, 180);
            var html = await ParseCianManager.GetHtmlAsync(url);
            var findedFlatDto = await FinderFlats.ExecuteAsync(html);

            await FlatsCreator.CreateAsync(findedFlatDto);
        }
    }
}
