using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NotiCore.API.Services;
using NotiCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly IScraperService _scrapperService;
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(
        IScraperService scrapperService,
        ILogger<WeatherForecastController> logger
        )
        {
            _scrapperService = scrapperService;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet]
        [Route("content/")]
        public async Task<ObjectResult> GetContentAsync([FromBody] string url)
        {
            try
            {
                url = "https://" + url + ".com";
                var content = await _scrapperService.ExtractText(url);
                return Ok(content);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
                throw;
            }
            
        }
    }
}
