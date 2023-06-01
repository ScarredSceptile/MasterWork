using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterTestingV2.APIModels
{
    public class AIModel
    {
        [LoadColumn(0)]
        public string Review { get; set; }
        [LoadColumn(1)]
        public float Score { get; set; }

        public AIModel() { }
        public AIModel(string review, float score)
        {
            Review = review;
            Score = score;
        }
    }
}
