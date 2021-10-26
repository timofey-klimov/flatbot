using AutoMapper;
using AutoMapper.QueryableExtensions;
using Entities.Models.Exceptions;
using Infrastructure.Interfaces.DataAccess;
using Infrastructure.Interfaces.Jobs;
using Infrastructure.Interfaces.Jobs.Dto;
using Infrastructure.Interfaces.Logger;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WepApp.JobManagers.Dto;

namespace WepApp.JobManagers.Base
{
    public abstract class BaseSheduleJobManager<T> : ISheduleJobManager
        where T : IJob
    {
        protected ILoggerService Logger;
        protected IServiceScopeFactory ScopeFactory;
        protected T HandleJobService;
        protected IWebHostEnvironment WebHostEnvironment;

        public event Action<Type, JobStatusDto, string> FinishEvent;

        public BaseSheduleJobManager(
            ILoggerService logger,
            IServiceScopeFactory serviceScopeFactory,
            IWebHostEnvironment env)
        {
            Logger = logger;
            ScopeFactory = serviceScopeFactory;
            WebHostEnvironment = env;
        }

        protected abstract CanExecuteResult CanExecute(ICollection<JobManagerDto> runningJobs);

        public async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Logger.Info(this.GetType(), $"Start {typeof(T).Name}");

            using (var scope = ScopeFactory.CreateScope())
            {
                try
                {
                    var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
                    var dbContext = scope.ServiceProvider.GetRequiredService<IDbContext>();

                    var jobManagers = dbContext
                        .SheduleJobManagers
                        .Where(x => x.IsRunning)
                        .ProjectTo<JobManagerDto>(mapper.ConfigurationProvider)
                        .ToList();

                    var canExecuteResult = CanExecute(jobManagers);

                    if (!WebHostEnvironment.IsDevelopment() && canExecuteResult.CanExecute == false)
                    {
                        FinishEvent?.Invoke(this.GetType(), canExecuteResult.Status.Value, default);
                        return;
                    }
                   
                    var service = scope.ServiceProvider.GetRequiredService<T>();
                    await service.ExecuteAsync(stoppingToken);

                    FinishEvent?.Invoke(this.GetType(), JobStatusDto.Success, default);
                }
                catch (Exception ex)
                {
                    Logger.Error(this.GetType(), ex.Message);

                    string message;
                    if (ex is ExceptionBase eBase)
                    {
                        message = eBase.Message;
                    }
                    else
                    {
                        message = "Internal";
                    }

                    FinishEvent?.Invoke(this.GetType(), JobStatusDto.Fail, message);
                }
            }
        }
    }
}
