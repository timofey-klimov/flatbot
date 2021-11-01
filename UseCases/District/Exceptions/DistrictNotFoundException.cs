using Entities.Exceptions.Base;
using Entities.Models.Exceptions;

namespace UseCases.District.Exceptions
{
    public class DistrictNotFoundException : EntityNotFoundException<string>
    {
        public DistrictNotFoundException(string dstrictName)
            : base(dstrictName, typeof(Entities.Models.District))
        {

        }
    }
}
