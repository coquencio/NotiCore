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
    public class WorkflowController : Controller
    {
        private readonly ISubscriberService _subscriberService;
        private readonly IViewModelService _viewModelService;
        public WorkflowController(ISubscriberService subscriberService, IViewModelService viewModelService)
        {
            _subscriberService = subscriberService;
            _viewModelService = viewModelService;
        }
        [HttpPost]
        [Route("Enroll")]
        [Authorize(Policy = "Admin")]
        public async Task EnrollSubscriber(AddSubscriberRequest request)
        {
            var action = Url.GenerateAbsoluteUrl("api", new string[] { "Workflow", "SetupSources"});
            await _subscriberService.EnrollAsync(request, action);
        }
        [HttpGet]
        [Route("SetupSources")]
        [AllowAnonymous]
        public IActionResult SetupSubscriber([FromQuery] string values = null)
        {
            var vm = _viewModelService.GetUserSourceSetupModel(values);
            return View(vm);
        }

    }
}
