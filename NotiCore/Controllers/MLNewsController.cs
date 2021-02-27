using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NotiCore.API.Infraestructure.Response;
using NotiCore.API.Models.MachineLearning.PredictNewsWebsite;
using NotiCore.API.Models.Requests;
using NotiCore.API.Services.ControllerServices;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace NotiCore.API.Controllers
{
    [Route("api/NewsPredictions")]
    [Authorize]
    [ApiController]
    public class MLNewsController : ControllerBase
    {
        private readonly ILogger<MLNewsController> _logger;

        private readonly IPredictNewsWebsiteService _predictService;
        public MLNewsController(IPredictNewsWebsiteService predictService, ILogger<MLNewsController> logger)
        {
            _logger = logger;
            _predictService = predictService;
        }

        [HttpGet]
        [Route("PredictSite")]
        public async Task<BaseResponse<PredictNewsWebsiteOutput>> GuessUrlAsync([FromBody] PredictNewsWebsiteRequest urlRequest)
        {
            try
            {
                var prediction = await _predictService.GetPredictionFromUrlAsync(urlRequest.Url);
                return new BaseResponse<PredictNewsWebsiteOutput>(prediction, "Model predicted");

            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error ocurred when trying to pull data from {url}", $"url: {urlRequest.Url}");
                return new BaseResponse<PredictNewsWebsiteOutput>(null, ex.Message)
                     .BadRequest();
                throw;
            }
            catch (Exception ex)
            {
                // ToDo Log errors into response wrapper
                _logger.LogError(ex, "Error ocurred when trying to pull data from {url}", $"url: {urlRequest.Url}");
                return new BaseResponse<PredictNewsWebsiteOutput>(null, "Unknown Error")
                     .InternalError();
                throw;
            }
        }
    }
}
