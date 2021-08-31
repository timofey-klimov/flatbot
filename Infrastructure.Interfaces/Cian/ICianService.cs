using Infrastructure.Interfaces.Cian.Enums;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Cian
{
    public interface ICianService
    {
        public Task<int?> GetPagesCount(City city, DealType dealType, Room room);
    }
}
