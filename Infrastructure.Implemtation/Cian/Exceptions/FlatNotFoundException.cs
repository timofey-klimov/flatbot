using Entities.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implemtation.Cian.Exceptions
{
    public class FlatNotFoundException : ExceptionBase
    {
        public FlatNotFoundException(int id)
            :base($"Flat not found {id}")
        {

        }
    }
}
