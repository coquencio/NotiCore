using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Models.Requests
{
    public class AddSourceRequest
    {
        public string Url { get; set; }
        public int LanguageId { get; set; }
        public string Name { get; set; }
    }
}
