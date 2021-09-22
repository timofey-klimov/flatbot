using Infrastructure.Interfaces.Jobs;
using Microsoft.AspNetCore.Hosting;
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
        private IWebHostEnvironment _env;

        public JobsQueue(IEnumerable<ISheduleJobManager> jobs, IWebHostEnvironment env)
        {
            _jobs = jobs;
            _indexCurrentJob = 0;
            _env = env;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //if (_env.IsDevelopment())
            //    return;

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
