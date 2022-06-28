using Microsoft.ML.Data;

namespace MachineLearning_API.Schema
{
    public class ModelInput
    {
        [ColumnName(@"Label")]
        public string Label { get; set; }

        [ColumnName(@"ImageSource")]
        public string ImageSource { get; set; }

    }
}