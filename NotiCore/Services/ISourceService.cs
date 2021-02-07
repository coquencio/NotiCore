using NotiCore.API.Models.DataContext;
using NotiCore.API.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Services
{
    public interface ISourceService
    {
        Task<Source> AddSourceAsync(AddSourceRequest SourceRequest);
        Task<bool> IsValidNewsSiteAsync(string url);
    }
}
