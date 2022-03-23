using NotiCore.API.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Services.ControllerServices
{
    public interface ITokenService
    {
        public string GetSubscriberToken(string mail);
        public string GetUserToken(string userName, string password);
        string GetUserToken(AddUserRequest request);
    }
}
