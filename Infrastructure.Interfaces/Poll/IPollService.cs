using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Poll
{
    public interface IPollService
    {
        Task<T> Execute<T>(Func<Task<PollResult<T>>> action,
            Func<Task> retryAction);

        Task<T> Execute<T, V>(Func<V, Task<PollResult<T>>> action, V arg,
            Func<Task> retryAction);
    }
}
