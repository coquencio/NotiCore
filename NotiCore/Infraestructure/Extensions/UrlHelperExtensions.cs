using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Infraestructure.Extensions
{
    public static class UrlHelperExtensions
    {
        public static string GenerateAbsoluteUrl(this IUrlHelper helper, string path, string [] subRoutes = null, bool forceHttps = false)
        {
            const string HTTPS = "https";
            var uri = helper.ActionContext.HttpContext.Request;
            var scheme = forceHttps ? HTTPS : uri.Scheme;
            var host = uri.Host;
            var port = (forceHttps || uri.Scheme == HTTPS) ? string.Empty : (uri.Host.Port == 80 ? string.Empty : ":" + uri.Host.Port);

            if(subRoutes == null)
                return string.Format("{0}://{1}{2}/{3}", scheme, host, port, string.IsNullOrEmpty(path) ? string.Empty : path.TrimStart('/'));

            var url = string.Format("{0}://{1}{2}/{3}", scheme, host, port, string.IsNullOrEmpty(path) ? string.Empty : path.TrimStart('/'));

            foreach (var route in subRoutes)
            {
                url += string.Format("/{0}", route);
            }
            return url;
        }

    }
}
