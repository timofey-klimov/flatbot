using Infrastructure.Interfaces.Logger;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using UseCases.Flats.BackgroundJobs;
using WepApp.JobManagers.Base;
using WepApp.JobManagers.Dto;

namespace WepApp.JobManagers
{
    public class ParseCianJobManager : BaseSheduleJobManager<ParseCianRentFlatJob>
    {
        public ParseCianJobManager(
            ILoggerService logger,
            IServiceScopeFactory serviceScopeFactory)
            :base(logger, serviceScopeFactory)
        {

        }

        public override CanExecuteResult CanExecute(ICollection<JobManagerDto> runningJobs)
        {
            if (DateTime.Now.AddHours(3).Hour < 8)
                return CanExecuteResult.JobCannotExecute(Infrastructure.Interfaces.Jobs.Dto.JobStatusDto.DateTimeNotInRange);

            return CanExecuteResult.JobCanExecute();
        }
    }
}
