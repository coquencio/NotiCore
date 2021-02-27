using Microsoft.ML;
using NotiCore.API.Models.MachineLearning.PredictNewsWebsite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Services.CoreServices.Implementation
{
    public class MLNewsWebsiteModel : IMLNewsWebsiteModel
    {
        private Lazy<PredictionEngine<PredictNewsWebsiteInput, PredictNewsWebsiteOutput>> _predictionEngine;
        private ITransformer _mlModel;
        private MLContext _mLContext;

        public MLNewsWebsiteModel(string path)
        {
            _predictionEngine = new Lazy<PredictionEngine<PredictNewsWebsiteInput, PredictNewsWebsiteOutput>>(CreatePredictionEngine);
            _mLContext = new MLContext();
            _mlModel = _mLContext.Model.Load(path, out var modelInputSchema);
        }
        // For more info on consuming ML.NET models, visit https://aka.ms/mlnet-consume
        // Method for consuming model in your app
        public  PredictNewsWebsiteOutput Predict(PredictNewsWebsiteInput input)
        {
            PredictNewsWebsiteOutput result = _predictionEngine.Value.Predict(input);
            return result;
        }

        public PredictionEngine<PredictNewsWebsiteInput, PredictNewsWebsiteOutput> CreatePredictionEngine()
        {
            var predEngine = _mLContext.Model.CreatePredictionEngine<PredictNewsWebsiteInput, PredictNewsWebsiteOutput>(_mlModel);
            return predEngine;
        }
    }
}
