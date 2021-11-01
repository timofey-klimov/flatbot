using Entities.Models;
using Entities.Models.Base;
using Entities.Models.Exceptions;
using System;

namespace Entities.Exceptions.Base
{
    public abstract class EntityNotFoundException<V> : EntityNotFoundExceptionBase
    {
        public EntityNotFoundException(V id, Type entity) 
            : base($"{entity.Name} not found {id}")
        {
        }
    }
}
