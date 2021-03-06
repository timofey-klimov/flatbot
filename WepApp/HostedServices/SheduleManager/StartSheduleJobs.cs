using AutoMapper;
using Entities.Enums;
using Entities.Models;
using Infrastructure.Interfaces.DataAccess;
using Infrastructure.Interfaces.Jobs;
using Infrastructure.Interfaces.Jobs.Dto;
using Infrastructure.Interfaces.Logger;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WepApp.HostedServices.Exceptions;

namespace WepApp.HostedServices.SheduleManager
{
    public class StartSheduleJobs : IHostedService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IWebHostEnvironment _env;
        private Timer _timer;

        public StartSheduleJobs(IServiceScopeFactory serviceScopeFactory, IWebHostEnvironment webHostEnvironment)
        {
            _scopeFactory = serviceScopeFactory;
            _env = webHostEnvironment;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(StartJobs, cancellationToken, TimeSpan.Zero, TimeSpan.FromMinutes(1));
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private void StartJobs(object state)
        {
            //if (_env.IsDevelopment())
            //    return;

            var token = (CancellationToken)state;

            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<IDbContext>();
                var logger = scope.ServiceProvider.GetRequiredService<ILoggerService>();

                var jobs = dbContext.SheduleJobManagers
                    .Include(x => x.JobHistories)
                    .Where(x => x.IsActive && !x.IsRunning)
                    .ToList()
                    .Where(x => 
                        x.PlanningRunTime == null ||
                        (x.PlanningRunTime.Value.ToShortDateString() == DateTime.Now.ToShortDateString()
                        &&
                        x.PlanningRunTime.Value.ToShortTimeString() == DateTime.Now.ToShortTimeString()))
                    .OrderBy(x => x.OrderNumber)
                    .ToList();

                foreach (var job in jobs)
                {
                    try
                    {
                        var jobManagerType = Type.GetType($"WepApp.JobManagers.{job.JobManagerName}");

                        var jobManagerInstanse = scope.ServiceProvider.GetService(jobManagerType) as ISheduleJobManager;

                        if (jobManagerInstanse == null)
                            throw new JobManagerNotFoundException(job.JobManagerName);

                        jobManagerInstanse.FinishEvent += Finish_Job;

                        job.RunTime = DateTime.Now;
                        job.IsRunning = true;
                        job.CreateJobHistory();

                        dbContext.SaveChangesAsync().Wait();

                        jobManagerInstanse.ExecuteAsync(token);
                    }
                    catch (Exception ex)
                    {
                        logger.Info(this.GetType(), ex.Message);
                    }
                }
            }
        }

        private void Finish_Job(Type jobManagerType, JobStatusDto jobStatusDto, string message)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<IDbContext>();

                var jobManager = dbContext.SheduleJobManagers
                    .Include(x => x.JobHistories)
                    .FirstOrDefault(x => x.JobManagerName == jobManagerType.Name);

                if (jobManager == null)
                    throw new JobManagerNotFoundException(jobManagerType.Name);

                var nextFireAt = TimeSpan.FromMinutes(jobManager.Period);

                if (jobStatusDto != JobStatusDto.Concurrent)
                {
                    if (jobManager is TimedJobManager timeJob)
                    {
                        timeJob.SheduleRunTime += TimeSpan.FromHours(timeJob.Period);
                        timeJob.PlanningRunTime = timeJob.SheduleRunTime;
                    }
                    else
                    {
                        if (jobManager.PlanningRunTime == null || jobManager.PlanningRunTime + nextFireAt < DateTime.Now)
                            jobManager.PlanningRunTime = jobManager.RunTime + nextFireAt;
                        else
                            jobManager.PlanningRunTime = DateTime.Now + nextFireAt;
                    }
                }
                else
                {
                    jobManager.PlanningRunTime += TimeSpan.FromMinutes(5);
                }

                switch (jobStatusDto)
                {
                    case JobStatusDto.Success:
                        jobManager.UpdateJobHistoryToSuccess(DateTime.Now);
                        break;
                    case JobStatusDto.Fail:
                        jobManager.UpdateJobHistoryToFail(DateTime.Now, message);
                        break;
                    case JobStatusDto.Concurrent:
                        jobManager.UpdateJobHistoryToConcurrent(DateTime.Now);
                        break;
                    case JobStatusDto.DateTimeNotInRange:
                        jobManager.UpdateJobHistoryToDateTimeNotInRange(DateTime.Now);
                        break;
                }

                jobManager.IsRunning = false;

                dbContext.SaveChangesAsync().Wait();
            }
        }
    }
}
