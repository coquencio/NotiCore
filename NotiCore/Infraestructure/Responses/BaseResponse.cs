using Microsoft.AspNetCore.Mvc;
using NotiCore.API.Infraestructure.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Infraestructure.Response
{
    public class BaseResponse<T> : ObjectResult
    {
        private ResponseWrapper<T> Response;
        public BaseResponse(T value, string message = null) : base(value)
        {
            StatusCode = 200;
            Response = new ResponseWrapper<T>(value, message, StatusCode);
            Value = Response;
        }
        public BaseResponse<T> BadRequest()
        {
            StatusCode = 400;
            Response.StatusCode = StatusCode;

            return this;
        }
        public BaseResponse<T> InternalError()
        {
            StatusCode = 500;
            Response.StatusCode = StatusCode;
            return this;
        }

    }
}
