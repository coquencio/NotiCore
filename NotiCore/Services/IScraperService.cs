using NotiCore.API.Models.ScrappedArticles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Services
{
    public interface IScraperService
    {
        Task<string> ExtractWordsFromUrlAsync(string url);
        IEnumerable<RTExample> ExtractRTNews(string url);
    }
}
