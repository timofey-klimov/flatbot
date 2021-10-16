using Infrastructure.Interfaces.Logger;
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
            IServiceScopeFactory serviceScopeFactory)
            :base(logger, serviceScopeFactory)
        {

        }

        public override CanExecuteResult CanExecute(ICollection<JobManagerDto> runningJobs)
        {
            var concurrentJob = runningJobs.FirstOrDefault(x => x.Name == nameof(ParseCianJobManager));

            if (concurrentJob != null)
                return CanExecuteResult.JobCannotExecute(Infrastructure.Interfaces.Jobs.Dto.JobStatusDto.Concurrent);

            return CanExecuteResult.JobCanExecute();
        }
    }
}
