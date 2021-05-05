using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Models.ViewModel
{
    public class SourceSetupViewModel
    {
        public string User { get; set; }
        public string Name { get; set; }
        public List<(int sourceId, string displayName, bool selected)> Sources { get; set; }
        public bool Expired { get; set; }
    }
}
