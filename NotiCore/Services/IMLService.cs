using NotiCoreML.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Services
{
    public interface IMLService
    {
        Task<ModelOutput> GetPredictionFromUrlAsync(string url);
    }
}
