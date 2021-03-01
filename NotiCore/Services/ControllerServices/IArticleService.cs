using NotiCore.API.Models.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Services.ControllerServices
{
    public interface IArticleService
    {
        Task SaveArticlesFromSourceAsync(Source source);
    }
}
