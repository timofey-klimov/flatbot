using Infrastructure.Interfaces.Logger;
using Microsoft.Extensions.DependencyInjection;
using System;
using UseCases.Flats.BackgroundJobs;
using WepApp.JobManagers.Base;

namespace WepApp.JobManagers
{
    public class ParseCianJobManager : BaseSheduleJobManager<ParseCianRentFlatJob>
    {
        public ParseCianJobManager(
            ILoggerService logger,
            IServiceScopeFactory serviceScopeFactory,
            int hours)
            :base(logger, serviceScopeFactory, TimeSpan.FromHours(hours))
        {

        }

        public override bool CanExecute()
        {
            return (DateTime.Now.AddHours(3).Hour > 8);
        }
    }
}
