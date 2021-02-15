using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Models.Requests
{
    public class AddUserRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
