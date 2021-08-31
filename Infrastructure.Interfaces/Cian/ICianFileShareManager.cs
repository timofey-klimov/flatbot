using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Cian
{
    public interface ICianFileShareManager
    {
        Task<string> SaveFileAsync(byte[] bytes);
    }
}
