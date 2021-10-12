using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Jobs
{
    public interface IJobStateManager
    {
        bool IsRunning { get; }

        void RunJob();

        void FinishJob();
    }
}
