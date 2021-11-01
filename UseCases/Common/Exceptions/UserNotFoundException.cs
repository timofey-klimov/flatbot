using Entities.Exceptions.Base;
using Entities.Models.Exceptions;

namespace UseCases.Common.Exceptions
{
    public class UserNotFoundException : EntityNotFoundException<long>
    {
        public UserNotFoundException(long id)
            : base(id, typeof(Entities.Models.User))
        {

        }
    }
}
