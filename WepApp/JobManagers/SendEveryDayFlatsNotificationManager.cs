using Infrastructure.Interfaces.Logger;
using Microsoft.Extensions.DependencyInjection;
using System;
using UseCases.Notifications.Jobs;
using WepApp.JobManagers.Base;

namespace WepApp.JobManagers
{
    public class SendEveryDayFlatsNotificationManager : BaseSheduleJobManager<SendEveryDayFlatsNotificationJob>
    {
        public SendEveryDayFlatsNotificationManager(
            ILoggerService logger,
            IServiceScopeFactory serviceScopeFactoy,
            int hours)
            : base(logger, serviceScopeFactoy, TimeSpan.FromHours(hours))
        {

        }

        public override bool CanExecute()
        {
            return (DateTime.Now.Hour > 8);
        }
    }
}
