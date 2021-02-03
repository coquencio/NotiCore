using NotiCore.API.Models.MachineLearning.PredictNewsWebsite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Services
{
    public interface IPredictNewsWebsiteService
    {
        Task<PredictNewsWebsiteOutput> GetPredictionFromUrlAsync(string url);
    }
}
