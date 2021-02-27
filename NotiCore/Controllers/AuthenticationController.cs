using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NotiCore.API.Infraestructure.Response;
using NotiCore.API.Models.Requests;
using NotiCore.API.Models.Response;
using NotiCore.API.Services.ControllerServices;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NotiCore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly ILogger<AuthenticationController> _logger;
        public AuthenticationController(ITokenService tokenService, ILogger<AuthenticationController> logger)
        {
            _tokenService = tokenService;
            _logger = logger;
        }

        [HttpPost]
        [Route("Login")]
        public BaseResponse<TokenResponse> Login([FromBody] AddUserRequest addUserRequest)
        {
            try
            {
                var token = new TokenResponse
                {
                    Token = _tokenService.GetUserToken(addUserRequest)
                };
                return new BaseResponse<TokenResponse>(token, "Token generated");
            }
            catch (KeyNotFoundException ex)
            {
                return new BaseResponse<TokenResponse>(null, ex.Message)
                    .NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ocurred when trying to login {user}", $"user: {addUserRequest}");
                return new BaseResponse<TokenResponse>(null, "Unknown Error")
                    .InternalError();
            }
        }
    }
}
