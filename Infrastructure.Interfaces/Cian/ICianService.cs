using Infrastructure.Interfaces.Cian.Enums;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Cian
{
    public interface ICianService
    {
        Task<int?> GetPagesCount(City city);
    }
}
