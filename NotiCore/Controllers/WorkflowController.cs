using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotiCore.API.Infraestructure.Extensions;
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
    public class WorkflowController : ControllerBase
    {
        private readonly ISubscriberService _subscriberService;
        public WorkflowController(ISubscriberService subscriberService)
        {
            _subscriberService = subscriberService;
        }
        [HttpPost]
        [Route("Enroll")]
        [AllowAnonymous]
        public async Task EnrollSubscriber(AddSubscriberRequest request)
        {
            var action = Url.GenerateAbsoluteUrl("api", new string[] { "Workflow", "SetupSources" });
            await _subscriberService.EnrollAsync(request, action);
        }
        [HttpGet]
        [Route("SetupSources")]
        public void SetupSubscriber([FromQuery] string values = null)
        {
            throw new NotImplementedException();
        }

    }
}
