using Infrastructure.Interfaces.Cian.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Cian
{
    public interface IParseCianManager
    {
        Task<int> GetPagesCountAsync(City city);

        string BuildCianUrl(City city, int page);

        Task<string> GetHtmlAsync(string url);

        Task<bool> CheckAnnouncement(string url);

        Task ClearDatabase();

        Task<ICollection<string>> GetCianImagesAsync(string url);
    }
}
