using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NotiCore.API.Infraestructure.Response;
using NotiCore.API.Models.Requests;
using NotiCore.API.Services.ControllerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;
        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }
        [HttpPost]
        [Route("Add")]
        [Authorize(Policy = "Admin")]
        public BaseResponse<string> Register([FromBody] AddUserRequest userRequest)
        {
            try
            {
                _userService.AddUser(userRequest);
                return new BaseResponse<string>(null, "User Added")
                    .Created();
            }

            catch (ValidationException ex)
            {
                return new BaseResponse<string>(null, ex.Message)
                    .BadRequest(ex.Errors.Select(e => e.ErrorMessage));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ocurred when trying to add user {user}", $"user: {userRequest}");
                return new BaseResponse<string>(null, "Unknown Error")
                    .InternalError();
            }
        }
        [HttpPost]
        [Route("AddFirst")]
        [AllowAnonymous]
        public BaseResponse<string> Register([FromQuery] string key, [FromBody] AddUserRequest userRequest)
        {
            try
            {
                _userService.AddUser(key, userRequest);
                return new BaseResponse<string>(null, "User Added")
                    .Created();
            }

            catch (ValidationException ex)
            {
                return new BaseResponse<string>(null, ex.Message)
                    .BadRequest(ex.Errors.Select(e => e.ErrorMessage));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ocurred when trying to add user {user}", $"user: {userRequest}");
                return new BaseResponse<string>(null, "Unknown Error")
                    .InternalError();
            }
        }
    }
}
