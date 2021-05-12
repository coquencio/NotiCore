using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotiCore.API.Infraestructure.Response;
using NotiCore.API.Models.Response;
using NotiCore.API.Services.ControllerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletsController : ControllerBase
    {
        private readonly IWalletService _walletService;

        public WalletsController(IWalletService walletService)
        {
            _walletService = walletService;
        }
        [HttpGet]
        [Route("")]
        public BaseResponse<WalletResponse> GetAddress()
        {
            try
            {
                var wallets = _walletService.GetWallet();
                return new BaseResponse<WalletResponse>(wallets);
            }
            catch (Exception)
            {
                return new BaseResponse<WalletResponse>(null, "Unexpected Error happened")
                    .InternalError();
            }
        }
    }
}
