using Infrastructure.Interfaces.Logger;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using UseCases.Proxies.Jobs;
using WepApp.JobManagers.Base;
using WepApp.JobManagers.Dto;

namespace WepApp.JobManagers
{
    public class UpdateProxiesJobManager : BaseSheduleJobManager<UpdateProxiesJob>
    {
        public UpdateProxiesJobManager(ILoggerService loggerService, IServiceScopeFactory serviceScopeFactory)
            : base(loggerService, serviceScopeFactory)
        {

        }
        public override CanExecuteResult CanExecute(ICollection<JobManagerDto> runningJobs)
        {
            return CanExecuteResult.JobCanExecute();
        }
    }
}
