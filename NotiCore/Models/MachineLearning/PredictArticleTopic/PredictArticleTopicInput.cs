using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Models.MachineLearning.PredictArticleTopic
{
    public class PredictArticleTopicInput
    {
        [ColumnName("topic"), LoadColumn(0)]
        public string Topic { get; set; }


        [ColumnName("text"), LoadColumn(1)]
        public string Text { get; set; }
    }
}
