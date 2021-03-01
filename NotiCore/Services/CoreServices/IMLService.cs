using Microsoft.ML;
using NotiCore.API.Models.MachineLearning.PredictArticleTopic;
using NotiCore.API.Models.MachineLearning.PredictNewsWebsite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Services.CoreServices
{ 

    public interface IMLService
    {
        PredictNewsWebsiteOutput NewsSitePrediction(PredictNewsWebsiteInput input);
        public PredictArticleTopicOutput PredictTopic(PredictArticleTopicInput input);
    }
}
