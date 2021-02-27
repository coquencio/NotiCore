using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Services.CoreServices
{
    public interface IPythonService
    {
        Dictionary<string, string> ExecutePythonCode(string[] libraries, string code, string[] values = null);
    }
}
