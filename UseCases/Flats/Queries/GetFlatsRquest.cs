using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.User.Base;

namespace UseCases.Flats.Queries
{
    public class GetFlatsRquest : BaseUserRequest<string>
    {
        public GetFlatsRquest(long chatId)
            : base(chatId)
        {

        }
    }
}
