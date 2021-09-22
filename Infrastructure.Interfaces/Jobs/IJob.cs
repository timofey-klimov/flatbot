using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Jobs
{
    public interface IJob
    {
        Task ExecuteAsync(CancellationToken token = default);
    }
}
