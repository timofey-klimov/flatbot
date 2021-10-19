using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestFixture]
    public class ProxyTest
    {
        private HttpClient _httpClient;
        const string hidemiAddress = "http://hidemy.name/ru/api/proxylist.php?out=js&type=5&code=658844074094668";
        [SetUp]
        public void Setup()
        {
            _httpClient = new HttpClient();
        }

        [Test]
        public async Task GetData()
        {
            var data = await _httpClient.GetAsync(hidemiAddress);

            var streamData = await data.Content.ReadAsStreamAsync();

            using (var streamReader = new StreamReader(streamData))
            {
                var result = await streamReader.ReadToEndAsync();
            }
        }
        
    }
}
