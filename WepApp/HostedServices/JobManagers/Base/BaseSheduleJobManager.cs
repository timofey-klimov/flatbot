using Entities.Models;
using Entities.Models.Exceptions;
using Infrastructure.Interfaces.DataAccess;
using Infrastructure.Interfaces.Jobs;
using Infrastructure.Interfaces.Logger;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WepApp.HostedServices.JobManagers.Base
{
    public abstract class BaseSheduleJobManager<T> : BackgroundService
        where T : IJob
    {
        private Timer _timer;
        private TimeSpan _period;
        protected ILoggerService Logger;
        protected IServiceScopeFactory ScopeFactory;
        protected T HandleJobService;


        public BaseSheduleJobManager(
            ILoggerService logger,
            IServiceScopeFactory serviceScopeFactory,
            TimeSpan period)
        {
            Logger = logger;
            ScopeFactory = serviceScopeFactory;
            _period = period;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(InternalExecute, null, TimeSpan.Zero, _period);

            return Task.CompletedTask;
        }

        private void InternalExecute(object state)
        {
            Logger.Info($"Start {typeof(T).Name}");

            using (var scope = ScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<IDbContext>();
                var job = typeof(T).Name;

                var history = new JobHistory
                {
                    Name = job,
                    StartDate = DateTime.Now,
                    Status = Entities.Enums.JobStatus.Running
                };

                dbContext.JobHistory.Add(history);
                dbContext.SaveChangesAsync(default).Wait();

                try
                {
                    var service = scope.ServiceProvider.GetRequiredService<T>();
                    service.Execute().Wait();

                    history.EndDate = DateTime.Now;
                    history.Status = Entities.Enums.JobStatus.Success;
                    dbContext.SaveChangesAsync(default).Wait();

                    Logger.Info($"Finish {typeof(T).Name}");
                }
                catch (Exception ex)
                {
                    string message = string.Empty;
                    if (ex.InnerException is ExceptionBase exBase)
                    {
                        message = exBase.Message;
                    }
                    else
                    {
                        message = "Internal";
                    }

                    history.Message = message;
                    history.EndDate = DateTime.Now;
                    history.Status = Entities.Enums.JobStatus.Fail;

                    dbContext.SaveChangesAsync(default).Wait();
                }
            }
        }
    }
}
