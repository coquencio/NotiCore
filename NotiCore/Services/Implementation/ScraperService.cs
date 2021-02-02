using NotiCore.API.Helpers;
using NotiCore.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace NotiCore.Services.Implementation
{
    public class ScraperService : IScraperService
    {
        private readonly HttpClient _client;
        public ScraperService(HttpClient client)
        {
            _client = client;
        }

        public async Task<string> ExtractText(string url)
        {
            if (UrlCustomHelper.IsValidUrl(url))
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13;
                _client.DefaultRequestHeaders.Accept.Clear();
                return await _client.GetStringAsync(url);
            }
            throw new Exception("Invalid Url provided");
        }

    }
}
