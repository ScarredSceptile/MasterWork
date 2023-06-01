using MasterTestingV2.APIModels;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.TorchSharp;
using Microsoft.ML.TorchSharp.NasBert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static MasterTestingV2.APIModels.AIModel;

namespace MasterTestingV2
{
    public class AI
    {
        public static void CreateModel(AIModel[] reviews, string file)
        {
            var ctx = new MLContext()
            {
                GpuDeviceId = 0,
                FallbackToCpu = true
            };

            IDataView trainingData = ctx.Data.LoadFromEnumerable<AIModel>(reviews);
            var pipeline = ctx.Transforms.Conversion.MapValueToKey(outputColumnName: "Label", inputColumnName: nameof(AIModel.Score))
            .Append(ctx.MulticlassClassification.Trainers.TextClassification(labelColumnName: "Label", sentence1ColumnName: "Review"))
            .Append(ctx.Transforms.Conversion.MapKeyToValue("PredictedLabel"))
            .AppendCacheCheckpoint(ctx);
            ITransformer trainedModel = pipeline.Fit(trainingData);
            ctx.Model.Save(trainedModel, null, file);
        }

        public static AIModel[] PredictScores(AIModel[] reviews, string trainedData)
        {
            MLContext mlContext = new MLContext();

            DataViewSchema predictionPipelineSchema;
            ITransformer predictionPipeline = mlContext.Model.Load(trainedData, out predictionPipelineSchema);

            IDataView data = mlContext.Data.LoadFromEnumerable(reviews);
            PredictionEngine<AIModel, AIOutput> predictionEngine = mlContext.Model.CreatePredictionEngine<AIModel, AIOutput>(predictionPipeline);
            var predictions = reviews.Select(n => predictionEngine.Predict(n)).ToArray();
            var transformedData = predictionPipeline.Transform(data);

            for (int i = 0; i < predictions.Count(); i++)
            {
                reviews[i].Score = predictions[i].PredictedScore[1];
                //reviews[i].Score = Math.Abs(reviews[i].Score - predictions[i].PredictedScore[1]);
            }
            return reviews;
        }
    }
}
