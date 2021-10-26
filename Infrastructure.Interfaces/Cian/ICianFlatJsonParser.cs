using Infrastructure.Interfaces.Cian.Dto;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Cian
{
    public interface ICianFlatJsonParser
    {
        Task<int> ParseAsync(JToken token, FindedFlatDto dto);
    }
}
