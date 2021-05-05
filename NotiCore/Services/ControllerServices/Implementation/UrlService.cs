using Microsoft.AspNetCore.Mvc;
using NotiCore.API.Services.CoreServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Services.ControllerServices.Implementation
{
    public class UrlService : IUrlService
    {
        private readonly IEncryptionService _encryptionService;
        public UrlService(IEncryptionService encryptionService)
        {
            _encryptionService = encryptionService;
        }

        public string BuildUrl(string route, DateTime Expiration, string parameterName, string parameterValue)
        {
            var keys = new List<(string key, string value)>();
            var formattedDate = Expiration.ToString("MM/dd/yyyy H:mm");
            keys.Add(("Expiration", formattedDate));
            keys.Add((parameterName, parameterValue));
            return route + "?values=" + _encryptionService.BuildKeys(keys);
        }
    }
}
