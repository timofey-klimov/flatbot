using Entities.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.Common.Exceptions
{
    public class UserNotFoundException : ExceptionBase
    {
        public UserNotFoundException(long chatId)
            : base($"No such user: {chatId}")
        {

        }
    }
}
