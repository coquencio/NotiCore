
using NotiCoreML.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Services.Implementation
{
    public class MLService : IMLService
    {
        private readonly IScraperService _scraperService;
        public MLService(IScraperService scraperService)
        {
            _scraperService = scraperService;
        }
        private ModelOutput PredictData(string words)
        {
            ModelInput sampleData = new ModelInput() 
            {
                Content = words,
            };

            // Make a single prediction on the sample data and print results
            var predictionResult = ConsumeModel.Predict(sampleData);

            return predictionResult;
        }

        public async Task<ModelOutput> GetPredictionFromUrlAsync(string url)
        {
            var words = await _scraperService.ExtractWordsFromUrlAsync(url);
            var predictions = PredictData(words);
            return predictions;
        }
    }
}
