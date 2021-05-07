using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NotiCore.API.Infraestructure.Extensions;
using NotiCore.API.Infraestructure.Response;
using NotiCore.API.Models.Requests;
using NotiCore.API.Services.ControllerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace NotiCore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkflowController : Controller
    {
        private readonly ISubscriberService _subscriberService;
        private readonly ILogger<WorkflowController> _logger;
        public WorkflowController(ISubscriberService subscriberService, ILogger<WorkflowController> logger)
        {
            _logger = logger;
            _subscriberService = subscriberService;
        }
        [HttpPost]
        [Route("Enroll")]
        [Authorize(Policy = "Admin")]
        public async Task<BaseResponse<string>> EnrollSubscriber(AddSubscriberRequest request)
        {
            try
            {
                var action = Url.GenerateAbsoluteUrl("Subscription", new string[] { "SetupSources" });
                await _subscriberService.EnrollAsync(request, action);
                return new BaseResponse<string>(null, "New email enrolled")
                .Created();
            }
            catch (ValidationException ex)
            {
                return new BaseResponse<string>(null, ex.Message)
                    .BadRequest(ex.Errors.Select(e => e.ErrorMessage));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ocurred while trying to Enroll subscriber", $"subscriber: {request}");
                return new BaseResponse<string>(null, "Unknown Error")
                    .InternalError();
            }
        }

    }
}
