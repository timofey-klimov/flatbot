using Infrastructure.Interfaces.Jobs;

namespace Infrastructure.Implemtation.Jobs
{
    public class JobStateManager : IJobStateManager
    {
        public bool IsRunning { get; set; }

        public void RunJob()
        {
            IsRunning = true;
        }

        public void FinishJob()
        {
            IsRunning = false;
        }
    }
}
