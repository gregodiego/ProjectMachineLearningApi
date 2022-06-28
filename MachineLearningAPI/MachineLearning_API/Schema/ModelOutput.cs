using Microsoft.ML.Data;

namespace MachineLearning_API.Schema
{
    public class ModelOutput
    {
        [ColumnName("PredictedLabel")]
        public string Prediction { get; set; }

        public float[] Score { get; set; }
    }
}
