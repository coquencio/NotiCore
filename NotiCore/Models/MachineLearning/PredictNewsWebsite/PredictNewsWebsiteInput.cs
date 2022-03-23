using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Models.MachineLearning.PredictNewsWebsite
{
    public class PredictNewsWebsiteInput
    {
        [ColumnName("content"), LoadColumn(0)]
        public string Content { get; set; }


        [ColumnName("target"), LoadColumn(1)]
        public string Target { get; set; }


        [ColumnName("source"), LoadColumn(2)]
        public string Source { get; set; }
    }
}
