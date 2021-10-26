using Infrastructure.Interfaces.Logger;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using UseCases.Notifications.Jobs;
using WepApp.JobManagers.Base;
using WepApp.JobManagers.Dto;

namespace WepApp.JobManagers
{
    public class SendEveryWeekFlatsNotificationManager : BaseSheduleJobManager<SendEveryWeekFlatsNotificationJob>
    {
        public SendEveryWeekFlatsNotificationManager(
            ILoggerService logger,
            IServiceScopeFactory serviceScopeFactory,
            IWebHostEnvironment webHostEnvironment)
            :base(logger, serviceScopeFactory, webHostEnvironment)
        {

        }

        protected override CanExecuteResult CanExecute(ICollection<JobManagerDto> runningJobs)
        {
            var concurrentJob = runningJobs.FirstOrDefault(x => x.Name == nameof(ParseCianJobManager));

            if (concurrentJob != null)
                return CanExecuteResult.JobCannotExecute(Infrastructure.Interfaces.Jobs.Dto.JobStatusDto.Concurrent);

            return CanExecuteResult.JobCanExecute();
        }
    }
}
