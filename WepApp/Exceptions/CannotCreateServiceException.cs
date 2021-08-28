using Entities.Models.Exceptions;
using System;

namespace WepApp.Exceptions
{
    public class CannotCreateServiceException : ExceptionBase
    {
        public CannotCreateServiceException(Type serviceType)
            :base($"Cannot create service typeof {serviceType.Name}")
        {
            
        }
    }
}
