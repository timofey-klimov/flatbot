using Entities.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.Common.Exceptions
{
    public class UserIsNullException : ExceptionBase
    {
        public UserIsNullException(long chatId)
            : base($"No such user: {chatId}")
        {

        }
    }
}
