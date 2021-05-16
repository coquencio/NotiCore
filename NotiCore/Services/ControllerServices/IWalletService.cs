using NotiCore.API.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Services.ControllerServices
{
    public interface IWalletService
    {
        WalletResponse GetWallet();
    }
}
