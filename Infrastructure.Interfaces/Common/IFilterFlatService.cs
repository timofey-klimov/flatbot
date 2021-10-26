using Entities.Models;
using Entities.Models.FlatEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Common
{
    public interface IFilterFlatService
    {
        Task<ICollection<Flat>> GetFlatsByUserContextAsync(UserContext context, int takeCount, CancellationToken token = default);
    }
}
