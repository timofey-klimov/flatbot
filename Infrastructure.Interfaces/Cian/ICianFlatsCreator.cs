using Infrastructure.Interfaces.Cian.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Cian
{
    public interface ICianFlatsCreator
    {
        Task CreateAsync(IEnumerable<FindedFlatDto> flats);
    }
}
