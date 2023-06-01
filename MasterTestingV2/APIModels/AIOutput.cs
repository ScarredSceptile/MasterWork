using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterTestingV2.APIModels
{
    public class AIOutput
    {
        [ColumnName("Score")]
        public float[] PredictedScore;
    }
}
