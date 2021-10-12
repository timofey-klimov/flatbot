using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WepApp.JobManagers.Dto;

namespace WepApp.JobManagers.Profile
{
    public class JobManagerProfile : AutoMapper.Profile
    {
        public JobManagerProfile()
        {
            CreateMap<SheduleJobManager, JobManagerDto>()
                .ForMember(x => x.Name, c => c.MapFrom(x => x.JobManagerName));
        }
    }
}
