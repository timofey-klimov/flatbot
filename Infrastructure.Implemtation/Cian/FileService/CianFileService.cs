using Entities.Models;
using Infrastructure.Implemtation.Cian.Exceptions;
using Infrastructure.Implemtation.Cian.FileService.Dto;
using Infrastructure.Interfaces.Cian;
using Infrastructure.Interfaces.Cian.FileService;
using Infrastructure.Interfaces.Cian.HttpClient;
using Infrastructure.Interfaces.Poll;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implemtation.Cian.FileService
{
    public class CianFileService : ICianFileService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ICianHttpClient _httpClient;
        private readonly IPollService _poll;
        private readonly IParseCianManager _cianParser;

        public CianFileService(
            IMemoryCache memoryCache,
            ICianHttpClient cianHttpClient,
            IPollService pollService,
            IParseCianManager cianParser)
        {
            _httpClient = cianHttpClient;
            _memoryCache = memoryCache;
            _poll = pollService;
            _cianParser = cianParser;
        }

        public async Task<ICollection<Stream>> GetCianFlatImagesAsync(Flat flat)
        {
            if (_memoryCache.TryGetValue<CianItemsDto>(flat, out var value) && value?.Images != null)
                return value.Images;

            if (flat.ImagesCollections.Value.Count == 0)
            {
                var urls = await _cianParser.GetCianImagesAsync(flat.CianReference);
                flat.ImagesCollections.Value.AddRange(urls);
            }

            var items = new List<Stream>(flat.ImagesCollections.Value.Count);

            foreach (var url in flat.ImagesCollections.Value)
            {
                var result = await _poll.Execute(PollGetCianFlatImageAsync, url, () =>
                {
                    _httpClient.CreateClientWithProxy();
                    return Task.CompletedTask;
                });

                if (result?.Length > 0)
                    items.Add(result);
            }

            var currentValue = value ?? new CianItemsDto();
            currentValue.Images = items;

            _memoryCache.Set(flat, currentValue);

            return items;
        }

        public async Task<byte[]> GetCianPdfAsync(Flat flat)
        {
            if (_memoryCache.TryGetValue<CianItemsDto>(flat, out var value) && value?.Pdf != null)
                return value.Pdf;

            var result = await _poll.Execute(PollGetCianPdfAsync, flat.PdfReference, () =>
            {
                _httpClient.CreateClientWithProxy();
                return Task.CompletedTask;
            });

            var currentValue = value ?? new CianItemsDto();
            currentValue.Pdf = result;

            _memoryCache.Set(flat, currentValue);

            return result;

        }

        private async Task<PollResult<byte[]>> PollGetCianPdfAsync(string url)
        {
            byte[] result = default;
            try
            {
                result = await _httpClient.GetFileInBytesAsync(url);
            }
            catch (HttpException ex)
            {
                return _poll.Fail<byte[]>(ex.Message);
            }

            return _poll.Ok(result);
        }

        private async Task<PollResult<Stream>> PollGetCianFlatImageAsync(string url)
        {
            Stream result = default;
            try
            {
                result = await _httpClient.GetFileInStreamAsync(url);
            }
            catch(HttpException ex)
            {
                return _poll.Fail<Stream>(ex.Message);
            }

            return _poll.Ok(result);
        }
    }
}
