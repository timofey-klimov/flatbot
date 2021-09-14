using Entities.Models;
using Entities.Models.Exceptions;
using Infrastructure.Interfaces.DataAccess;
using Infrastructure.Interfaces.Jobs;
using Infrastructure.Interfaces.Logger;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace WepApp.JobManagers.Base
{
    public abstract class BaseSheduleJobManager<T> : ISheduleJobManager
        where T : IJob
    {
        private Timer _timer;
        private TimeSpan _period;
        protected ILoggerService Logger;
        protected IServiceScopeFactory ScopeFactory;
        protected T HandleJobService;

        public event Action FinishEvent;

        public BaseSheduleJobManager(
            ILoggerService logger,
            IServiceScopeFactory serviceScopeFactory,
            TimeSpan period)
        {
            Logger = logger;
            ScopeFactory = serviceScopeFactory;
            _period = period;
        }

        public async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(InternalExecute, stoppingToken, TimeSpan.Zero, _period);
        }

        private void InternalExecute(object state)
        {
            var cts = (CancellationToken)state;
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
                dbContext.SaveChangesAsync(cts).Wait();

                try
                {
                    var service = scope.ServiceProvider.GetRequiredService<T>();
                    service.Execute(cts).Wait();

                    history.EndDate = DateTime.Now;
                    history.Status = Entities.Enums.JobStatus.Success;
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

                    dbContext.SaveChangesAsync(cts).Wait();
                }
                finally
                {
                    history.NextFireAt = DateTime.Now.Add(_period);
                    dbContext.SaveChangesAsync(cts).Wait();

                    Logger.Info($"Finish {typeof(T).Name}");
                    FinishEvent.Invoke();
                }
            }
        }
    }
}
