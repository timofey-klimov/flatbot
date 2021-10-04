using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.District.Dto;

namespace UseCases.District.Queries.GetDistricts
{
    public class GetDistrictsRequest : IRequest<ICollection<DistrictDto>>
    {
        
    }
}
