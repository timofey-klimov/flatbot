using Infrastructure.Interfaces.Jobs;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WepApp.HostedServices.Queue
{
    public class JobsQueue : BackgroundService
    {
        private readonly IEnumerable<ISheduleJobManager> _jobs;
        private int _indexCurrentJob;
        private CancellationToken _token;

        public JobsQueue(IEnumerable<ISheduleJobManager> jobs)
        {
            _jobs = jobs;
            _indexCurrentJob = 0;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _token = stoppingToken;

            foreach(var job in _jobs)
            {
                job.FinishEvent += Job_FinishEvent;
            }

            await _jobs.First().ExecuteAsync(stoppingToken); 
        }

        private void Job_FinishEvent()
        {
            _indexCurrentJob++;
            _jobs?.ElementAtOrDefault(_indexCurrentJob)?.ExecuteAsync(_token)?.Wait();
        }
    }
}
