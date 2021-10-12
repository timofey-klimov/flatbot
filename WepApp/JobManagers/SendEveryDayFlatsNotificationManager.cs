using Infrastructure.Interfaces.Logger;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using UseCases.Notifications.Jobs;
using WepApp.JobManagers.Base;
using WepApp.JobManagers.Dto;

namespace WepApp.JobManagers
{
    public class SendEveryDayFlatsNotificationManager : BaseSheduleJobManager<SendEveryDayFlatsNotificationJob>
    {
        public SendEveryDayFlatsNotificationManager(
            ILoggerService logger,
            IServiceScopeFactory serviceScopeFactoy)
            : base(logger, serviceScopeFactoy)
        {

        }

        public override CanExecuteResult CanExecute(ICollection<JobManagerDto> runningJobs)
        {
            var concurrentJob = runningJobs.FirstOrDefault(x => x.Name == nameof(ParseCianJobManager));

            if (concurrentJob != null)
                return CanExecuteResult.JobCannotExecute(Infrastructure.Interfaces.Jobs.Dto.JobStatusDto.Concurrent);

            if (DateTime.Now.AddHours(3).Hour <= 8)
                return CanExecuteResult.JobCannotExecute(Infrastructure.Interfaces.Jobs.Dto.JobStatusDto.DateTimeNotInRange);

            return CanExecuteResult.JobCanExecute();
        }
    }
}
