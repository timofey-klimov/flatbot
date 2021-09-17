using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.User.Base;

namespace UseCases.User.Commands.SetTimeToMetro
{
    public class SetTimeToMetroRequest : BaseUserRequest
    {
        public int TimeToMetro { get; }
        public SetTimeToMetroRequest(long chatId, int timeToMetro)
            : base(chatId)
        {
            TimeToMetro = timeToMetro;
        }
    }
}
