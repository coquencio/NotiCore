// This file was auto-generated by ML.NET Model Builder. 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.ML;
using NotiCoreML.Model;

namespace NotiCoreML.Model
{
    public class ConsumeModel
    {
        private static Lazy<PredictionEngine<NewsModelInput, NewsModelOutput>> PredictionEngine = new Lazy<PredictionEngine<NewsModelInput, NewsModelOutput>>(CreatePredictionEngine);

        // For more info on consuming ML.NET models, visit https://aka.ms/mlnet-consume
        // Method for consuming model in your app
        public static NewsModelOutput Predict(NewsModelInput input)
        {
            NewsModelOutput result = PredictionEngine.Value.Predict(input);
            return result;
        }

        public static PredictionEngine<NewsModelInput, NewsModelOutput> CreatePredictionEngine()
        {
            // Create new MLContext
            MLContext mlContext = new MLContext();

            // Load model & create prediction engine
            string modelPath = @"";
            ITransformer mlModel = mlContext.Model.Load(modelPath, out var modelInputSchema);
            var predEngine = mlContext.Model.CreatePredictionEngine<NewsModelInput, NewsModelOutput>(mlModel);

            return predEngine;
        }
    }
}
