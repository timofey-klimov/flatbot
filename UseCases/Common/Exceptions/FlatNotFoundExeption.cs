using Entities.Exceptions.Base;

namespace UseCases.Common.Exceptions
{
    public class FlatNotFoundExeption : EntityNotFoundException<int>
    {
        public FlatNotFoundExeption(int flatId)
            : base(flatId, typeof(Entities.Models.FlatEntities.Flat))
        {

        }
    }
}
