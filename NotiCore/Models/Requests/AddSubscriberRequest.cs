using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Models.Requests
{
    public class AddSubscriberRequest
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string  LastName { get; set; }
    }
}
