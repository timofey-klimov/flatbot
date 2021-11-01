using Entities.Models.Base;

namespace Entities.Models
{
    public abstract class Entity<T> : BaseEntity
        where T: struct
    {
        public T Id { get; set; }
    }
}
