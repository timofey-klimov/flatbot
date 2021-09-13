using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Jobs
{
    public interface IJob
    {
        Task Execute(CancellationToken token = default);
    }
}
