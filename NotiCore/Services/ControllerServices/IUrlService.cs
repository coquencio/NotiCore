using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Services.ControllerServices
{
    public interface IUrlService
    {
        string BuildUrl(string route, DateTime Expiration, string parameterName, string parameterValue);
    }
}
