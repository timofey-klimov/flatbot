using Entities.Models.Exceptions;

namespace UseCases.District.Exceptions
{
    public class DistrictNotFoundException : ExceptionBase
    {
        public DistrictNotFoundException(string message)
            : base(message)
        {

        }
    }
}
