using NotiCore.API.Models.DataContext;
using NotiCore.API.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Services.ControllerServices
{
    public interface ISourceService
    {
        Task<Source> AddSourceAsync(AddSourceRequest SourceRequest);
        IEnumerable<Source> GetSources(string query = null, int languageId = 0);
        Task<bool> IsValidNewsSiteAsync(string url);
    }
}
