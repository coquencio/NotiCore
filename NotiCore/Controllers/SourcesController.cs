using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NotiCore.API.Infraestructure.Response;
using NotiCore.API.Models.DataContext;
using NotiCore.API.Models.Requests;
using NotiCore.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NotiCore.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class SourcesController : ControllerBase
    {
        private readonly ISourceService _sourceService;
        private readonly ILogger<SourcesController> _logger;
        public SourcesController(ISourceService sourceService, ILogger<SourcesController> logger)
        {
            _logger = logger;
            _sourceService = sourceService;
        }

        [HttpPost]
        [Route("Add")]
        [Authorize(Policy = "Admin")]
        public async Task<BaseResponse<Source>> AddSourceAsync([FromBody] AddSourceRequest request)
        {
            try
            {
                var addedSource = await _sourceService.AddSourceAsync(request);
                return new BaseResponse<Source>(addedSource, "New source added").Created();
            }
            catch (ValidationException ex)
            {
                return new BaseResponse<Source>(null, ex.Message)
                    .BadRequest(ex.Errors.Select(e => e.ErrorMessage));
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error ocurred when trying to pull data from {url}", $"url: {request.Url}");
                return new BaseResponse<Source>(null, ex.Message)
                     .BadRequest();
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ocurred when trying to add source from request {request}", $"request: {request}");
                return new BaseResponse<Source>(null, ex.Message)
                    .InternalError();
            }
        }

        [HttpGet]
        public BaseResponse<IEnumerable<Source>> GetSources([FromQuery] string query, [FromQuery] int languageId = 0)
        {
            try
            {
                var sources = _sourceService.GetSources(query, languageId);
                return new BaseResponse<IEnumerable<Source>>(sources, "Retrieved Sources");
            }
            catch (ValidationException ex)
            {
                return new BaseResponse<IEnumerable<Source>>(null, ex.Message)
                    .BadRequest(ex.Errors.Select(e => e.ErrorMessage));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ocurred when trying to pull data from {query}", $"query: {query}");
                return new BaseResponse<IEnumerable<Source>>(null, "Unknown Error")
                    .InternalError();
            }
        }
    }
}
