using Infrastructure.Interfaces.Cian.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Cian
{
    public interface IFinderCianFlatsByHtml
    {
        Task<IEnumerable<FindedFlatDto>> ExecuteAsync(string html);
    }
}
