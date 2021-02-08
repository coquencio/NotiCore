﻿using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    [ApiController]
    public class SourcesController : ControllerBase
    {
        private readonly ISourceService _sourceService; 
        public SourcesController(ISourceService sourceService)
        {
            _sourceService = sourceService;
        }

        [HttpPost]
        [Route("Add")]
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
                return new BaseResponse<Source>(null, ex.Message)
                     .BadRequest();
                throw;
            }
            catch (Exception ex)
            {
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
            catch (Exception)
            {
                return new BaseResponse<IEnumerable<Source>>(null, "Unknown Error")
                    .InternalError();
            }
        }
    }
}
