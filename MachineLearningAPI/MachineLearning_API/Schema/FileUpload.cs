using Microsoft.AspNetCore.Http;

namespace MachineLearning_API.Schema
{
    public class FileUpload
    {
        public IFormFile files { get; set; }
    }
}
