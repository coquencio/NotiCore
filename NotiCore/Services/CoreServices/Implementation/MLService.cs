using Microsoft.ML;
using NotiCore.API.Models.MachineLearning.PredictArticleTopic;
using NotiCore.API.Models.MachineLearning.PredictNewsWebsite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Services.CoreServices.Implementation
{
    public class MLService : IMLService
    {
        private Lazy<PredictionEngine<PredictNewsWebsiteInput, PredictNewsWebsiteOutput>> _predictionEngine;
        private Lazy<PredictionEngine<PredictArticleTopicInput, PredictArticleTopicOutput>> _topicPredictionEngine;

        private ITransformer _mlModel;
        private ITransformer _topicModel;

        private MLContext _mLContext;

        public MLService(string newsPath, string topicPath)
        {
            _mLContext = new MLContext();

            // News validation Prediction
            _predictionEngine = new Lazy<PredictionEngine<PredictNewsWebsiteInput, PredictNewsWebsiteOutput>>(CreateNewsWebsitePredictionEngine);
            _mlModel = _mLContext.Model.Load(newsPath, out var modelInputSchema);

            // Topic Prediction
            _topicPredictionEngine = new Lazy<PredictionEngine<PredictArticleTopicInput, PredictArticleTopicOutput>>(CreateArticleTopicPredictionEngine);
            _topicModel = _mLContext.Model.Load(topicPath, out var modelTopicInputSchema);
        }
        // For more info on consuming ML.NET models, visit https://aka.ms/mlnet-consume
        // Method for consuming model in your app
        public  PredictNewsWebsiteOutput NewsSitePrediction(PredictNewsWebsiteInput input)
        {
            PredictNewsWebsiteOutput result = _predictionEngine.Value.Predict(input);
            return result;
        }
        public PredictArticleTopicOutput PredictTopic(PredictArticleTopicInput input)
        {
            PredictArticleTopicOutput result = _topicPredictionEngine.Value.Predict(input);
            return result;
        }
        private PredictionEngine<PredictNewsWebsiteInput, PredictNewsWebsiteOutput> CreateNewsWebsitePredictionEngine()
        {
            var predEngine = _mLContext.Model.CreatePredictionEngine<PredictNewsWebsiteInput, PredictNewsWebsiteOutput>(_mlModel);
            return predEngine;
        }
        private PredictionEngine<PredictArticleTopicInput, PredictArticleTopicOutput> CreateArticleTopicPredictionEngine()
        {
            var predEngine = _mLContext.Model.CreatePredictionEngine<PredictArticleTopicInput, PredictArticleTopicOutput>(_topicModel);
            return predEngine;
        }
    }
}
