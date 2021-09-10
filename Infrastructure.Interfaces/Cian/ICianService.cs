using Infrastructure.Interfaces.Cian.Enums;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Cian
{
    public interface ICianService
    {
        Task<int> GetPagesCountAsync(City city);

        Task<byte[]> GetExcelFromCianAsync(string url);

        string BuildCianUrl(City city, OperationType type, int page);
    }
}
