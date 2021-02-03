using Microsoft.ML;
using NotiCore.API.Models.MachineLearning.PredictNewsWebsite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Services
{
    public interface IMLNewsWebsiteModel
    {
        PredictNewsWebsiteOutput Predict(PredictNewsWebsiteInput input);
        PredictionEngine<PredictNewsWebsiteInput, PredictNewsWebsiteOutput> CreatePredictionEngine();
    }
}
