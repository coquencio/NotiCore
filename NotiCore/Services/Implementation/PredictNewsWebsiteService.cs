using NotiCore.API.Models.MachineLearning.PredictNewsWebsite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Services.Implementation
{
    public class PredictNewsWebsiteService : IPredictNewsWebsiteService
    {
        private readonly IScraperService _scraperService;
        private readonly IMLNewsWebsiteModel _mLNewsWebsiteModel;
        public PredictNewsWebsiteService(IScraperService scraperService, IMLNewsWebsiteModel mLNewsWebsiteModel)
        {
            _mLNewsWebsiteModel = mLNewsWebsiteModel;
            _scraperService = scraperService;
        }
        private PredictNewsWebsiteOutput PredictData(string words)
        {
            var sampleData = new PredictNewsWebsiteInput() 
            {
                Content = words,
            };

            // Make a single prediction on the sample data and print results
            var predictionResult = _mLNewsWebsiteModel.Predict(sampleData);

            return predictionResult;
        }

        public async Task<PredictNewsWebsiteOutput> GetPredictionFromUrlAsync(string url)
        {
            var words = await _scraperService.ExtractWordsFromUrlAsync(url);
            var predictions = PredictData(words);
            return predictions;
        }
    }
}
