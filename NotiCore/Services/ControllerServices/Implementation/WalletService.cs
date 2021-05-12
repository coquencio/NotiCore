using NotiCore.API.Models.Response;
using NotiCore.API.Services.CoreServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Services.ControllerServices.Implementation
{
    public class WalletService :IWalletService
    {
        private readonly IPropertiesService _propertiesService;
        public WalletService(IPropertiesService propertiesService)
        {
            _propertiesService = propertiesService;
        }
        public WalletResponse GetWallet()
        {
            var toReturn = new WalletResponse();
            toReturn.Bitcoin = _propertiesService.GetProperty("Bitcoin");
            toReturn.Ethereum = _propertiesService.GetProperty("Ethereum");
            return toReturn;
        }
    }
}
