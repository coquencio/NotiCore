using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NotiCore.API.Infraestructure.Response;
using NotiCore.API.Services;
using NotiCoreML.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NotiCore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly ILogger<NewsController> _logger;

        private readonly IMLService _mLService;
        public NewsController(IMLService mLService, ILogger<NewsController> logger)
        {
            _logger = logger;
            _mLService = mLService;
        }
        [Route("ValidateUrl")]
        public async Task<BaseResponse<ModelOutput>> GuessAsync([FromBody] string url)
        {
            try
            {
                var prediction = await _mLService.GetPredictionFromUrlAsync(url);
                return new BaseResponse<ModelOutput>(prediction, "Model predicted");

            }
            catch (HttpRequestException ex)
            {
                return new BaseResponse<ModelOutput>(null, ex.Message)
                     .BadRequest();
                throw;
            }
            catch (Exception)
            {
                return new BaseResponse<ModelOutput>(null, "Unknown Error")
                     .InternalError();
                throw;
            }
        }
    }
}
