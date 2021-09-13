using Infrastructure.Interfaces.Logger;
using Microsoft.Extensions.DependencyInjection;
using System;
using UseCases.Flats.BackgroundJobs;
using WepApp.HostedServices.JobManagers.Base;

namespace WepApp.HostedServices.JobManagers
{
    public class ClearDeletedAnnouncementJobManager : BaseSheduleJobManager<ClearDeletedAnnouncementJob>
    {
        public ClearDeletedAnnouncementJobManager(
            ILoggerService logger, 
            IServiceScopeFactory serviceScopeFactory, 
            TimeSpan period)
            : base(logger, serviceScopeFactory, period)
        {
        }
    }
}
