using Infrastructure.Interfaces.Cian.FileManager;
using Infrastructure.Interfaces.Cian.HttpClient;
using Infrastructure.Interfaces.Poll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implemtation.Cian.FileManager
{
    public class CianFileManager : ICianFileManager
    {
        private readonly ICianHttpClient _httpClient;
        private readonly IPollService _pollService;

        public CianFileManager(
            ICianHttpClient httpClient,
            IPollService pollService)
        {
            _httpClient = httpClient;
            _pollService = pollService;
        }

        public async Task<byte[]> GetFileAsync(string source)
        {
            var result = await _pollService.Execute(GetFilePoll, source, () =>
            {
                _httpClient.CreateClientWithProxy();
                return Task.CompletedTask;
            });

            return result;

        }

        #region poll

        private async Task<PollResult<byte[]>> GetFilePoll(string source)
        {
            try
            {
                var result = await _httpClient.GetFileInBytesAsync(source);

                return PollResult<byte[]>.Success(result);
            }
            catch (Exception ex)
            {
                return PollResult<byte[]>.Fail(ex.Message);
            }
        }
        #endregion
    }
}
