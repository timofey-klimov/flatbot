using Infrastructure.Interfaces.Cian;
using Infrastructure.Interfaces.Cian.Enums;
using Infrastructure.Interfaces.Cian.HttpClient;
using System.Threading.Tasks;

namespace UseCases.Flats.BackgroundJobs
{
    public class ParseCianRentFlatJob  
    {
        private ICianService _cianService;
        private ICianMapManager _cianMapManager;
        private ICianUrlBuilder _cianUrlBuilder;
        private ICianHttpClient _cianHttp;
        private ICianFileShareManager _fileShareManager;

        public ParseCianRentFlatJob(
            ICianService cianService,
            ICianUrlBuilder cianUrlBuilder,
            ICianMapManager cianMapManager,
            ICianHttpClient cianHttpClient,
            ICianFileShareManager fileShareManager)
        {
            _cianService = cianService;
            _cianUrlBuilder = cianUrlBuilder;
            _cianMapManager = cianMapManager;
            _cianHttp = cianHttpClient;
            _fileShareManager = fileShareManager;
        }

        public async Task Execute()
        {
            var cities = _cianMapManager.GetCities();

            Task.Run(async () =>
            {
                foreach (var city in cities)
                {
                    var pagesCount = await _cianService.GetPagesCount(city, DealType.Rent, Room.One);

                    for (int i = 0; i <= pagesCount; i++)
                    {
                        var url = _cianUrlBuilder.BuildCianUrl(city, DealType.Rent, Room.One, OperationType.GetExcel, i);
                        var excelInBytes = await _cianHttp.GetExcelFromCian(url);

                        var filePath = await _fileShareManager.SaveFileAsync(excelInBytes);

                    }

                    await Task.Delay(1000);
                }
            });
        }
    }
}
