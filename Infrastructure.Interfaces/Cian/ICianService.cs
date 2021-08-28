using Infrastructure.Interfaces.Cian.Enums;

namespace Infrastructure.Interfaces.Cian
{
    public interface ICianService
    {
        public int GetPagesCount(City city, DealType dealType, Room room);
    }
}
