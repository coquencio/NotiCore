using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotiCore.API.Services.CoreServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "Admin")]
    public class PropertiesController : ControllerBase
    {
        private readonly IPropertiesService _propertiesService;
        public PropertiesController(IPropertiesService propertiesService)
        {
            _propertiesService = propertiesService;
        }
        [HttpGet("Build/{key}/{value}")]
        public string BuildProperty(string key, string value)
        {
            _propertiesService.SaveProperty(key, value);
            return _propertiesService.GetProperty(key);
        }
        
    }
}
