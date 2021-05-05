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
    }
}
