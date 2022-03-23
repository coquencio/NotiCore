using Microsoft.AspNetCore.Mvc;
using NotiCore.API.Models.Responses;
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
        public BaseResponse<T> BadRequest(IEnumerable<string> errors)
        {
            StatusCode = 400;
            Response.StatusCode = StatusCode;
            Response.Errors = errors;
            return this;
        }
        public BaseResponse<T> BadRequest(params string[] errors)
        {
            var errorList = new List<string>();
            foreach (var error in errors)
            {
                errorList.Add(error);
            }
            StatusCode = 400;
            Response.StatusCode = StatusCode;
            Response.Errors = errorList;
            return this;
        }
        public BaseResponse<T> InternalError()
        {
            StatusCode = 500;
            Response.StatusCode = StatusCode;
            return this;
        }
        public BaseResponse<T> Created()
        {
            StatusCode = 201;
            Response.StatusCode = StatusCode;
            return this;
        }
        public BaseResponse<T> NotFound()
        {
            StatusCode = 404;
            Response.StatusCode = StatusCode;
            return this;
        }

        internal BaseResponse<string> BadRequest(object p)
        {
            throw new NotImplementedException();
        }
    }
}
