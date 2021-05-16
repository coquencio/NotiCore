using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotiCore.API.Services.ControllerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Controllers.API
{
    [Route("[controller]")]
    [ApiController]
    public class SubscriptionController : Controller
    {
        private readonly IViewModelService _viewModelService;
        public SubscriptionController(IViewModelService viewModelService)
        {
            _viewModelService = viewModelService;
        }
        [HttpGet]
        [Route("SetupSources")]
        [AllowAnonymous]
        public IActionResult SetupSubscriber([FromQuery] string values = null)
        {
            var vm = _viewModelService.GetUserSourceSetupModel(values);
            return View(vm);
        }
        [HttpPost]
        public IActionResult SaveSources([FromForm] IFormCollection values = null)
        {
            try
            {
                _viewModelService.SaveUserChanges(values);
                return View(false);
            }
            catch (Exception)
            {
                return View(true);
            }
        }
        [HttpGet]
        [Route("Deactivate")]
        [AllowAnonymous]
        public IActionResult DeactivateSubscriber([FromQuery] string values = null)
        {
            try
            {
                _viewModelService.DeactivateSubscriber(values);
                return View(true);
            }
            catch (Exception)
            {
                return View(false);
            }
        }
    }
}
