using Entities.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.Common.Exceptions
{
    public class FlatNotFoundExeption : ExceptionBase
    {
        public FlatNotFoundExeption(int flatId)
            : base($"Not found flat {flatId}")
        {

        }
    }
}
