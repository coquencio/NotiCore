﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Infraestructure.Responses
{
    public class ResponseWrapper<T>
    {
        public int? StatusCode { get; set; }
        public string Message { get; }
        public T Data { get; }

        public ResponseWrapper(T data, string message = null, int? statusCode = null)
        {
            Data = data;
            Message = message;
            StatusCode = statusCode;
        }
    }
}
