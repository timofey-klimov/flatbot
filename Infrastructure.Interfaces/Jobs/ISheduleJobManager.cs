using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Jobs
{
    public interface ISheduleJobManager
    {
        Task ExecuteAsync(CancellationToken stoppingToken);
        event Action FinishEvent;
    }
}
