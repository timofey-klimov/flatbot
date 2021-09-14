using Infrastructure.Interfaces.Logger;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
            TimeSpan period)
            :base(logger, serviceScopeFactory, period)
        {

        }

    }
}
