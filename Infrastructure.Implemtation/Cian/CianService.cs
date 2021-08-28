using Infrastructure.Interfaces.Cian;
using Infrastructure.Interfaces.Cian.Enums;

namespace Infrastructure.Implemtation.Cian
{
    public class CianService : ICianService
    {
        private readonly ICianUrlBuilder _cianUrlBuilder;
        public CianService(ICianUrlBuilder cianUrlBuilder)
        {
            _cianUrlBuilder = cianUrlBuilder;
        }

        public int GetPagesCount(City city, DealType dealType, Room room)
        {
            var url = _cianUrlBuilder.BuildCianUrl(city, dealType, room, 0);
            return 0;
        }
    }
}
