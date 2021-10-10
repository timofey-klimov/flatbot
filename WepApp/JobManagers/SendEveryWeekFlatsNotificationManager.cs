using Infrastructure.Interfaces.Logger;
using Microsoft.Extensions.DependencyInjection;
using System;
using UseCases.Notifications.Jobs;
using WepApp.JobManagers.Base;

namespace WepApp.JobManagers
{
    public class SendEveryWeekFlatsNotificationManager : BaseSheduleJobManager<SendEveryWeekFlatsNotificationJob>
    {
        public SendEveryWeekFlatsNotificationManager(
            ILoggerService logger,
            IServiceScopeFactory serviceScopeFactory,
            int period)
            :base(logger, serviceScopeFactory, TimeSpan.FromHours(period))
        {

        }
        public override bool CanExecute()
        {
            return (DateTime.Now.AddHours(3).Hour >= 8);
        }
    }
}
