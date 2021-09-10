using Infrastructure.Interfaces.Bus;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Cian
{
    public interface ICianExcelParser : IEventBusHandler
    {
        Task ParseAsync(byte[] bytes);
    }
}
