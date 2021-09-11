using Entities.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.Flats.BackgroundJobs.Exceptions
{
    public class FindZeroPagesException : ExceptionBase
    {
        public FindZeroPagesException(string message)
            : base(message)
        {

        }
    }
}
