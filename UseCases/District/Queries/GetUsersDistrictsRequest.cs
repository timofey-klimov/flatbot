using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.District.Dto;
using UseCases.User.Base;

namespace UseCases.District.Queries
{
    public class GetUsersDistrictsRequest : BaseUserRequest<ICollection<DistinctMenuDto>>
    {
        public GetUsersDistrictsRequest(long chatId)
            : base(chatId)
        {

        }
    }
}
