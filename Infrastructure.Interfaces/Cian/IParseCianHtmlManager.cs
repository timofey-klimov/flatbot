using Infrastructure.Interfaces.Cian.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Cian
{
    public interface IParseCianHtmlManager
    {
        Task<int> GetPagesCountAsync(City city);

        Task<string> GetHtmlAsync(string url);

        Task<bool> CheckAnnouncement(string url);

        Task<string> GetCianImageSourceAsync(string url);
    }
}
