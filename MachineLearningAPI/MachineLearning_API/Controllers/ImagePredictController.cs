using Microsoft.AspNetCore.Mvc;
using MachineLearning_API.Schema;
using Microsoft.Extensions.ML;
using System;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Reflection;

namespace MachineLearning_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImagePredictController : ControllerBase
    {
        private readonly PredictionEnginePool<ModelInput, ModelOutput> _predictionEnginePool;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private static readonly string path = @"Adicione o Path do diretório do seu projeto aqui";
        private static readonly string pathRoot = Path.GetPathRoot(path);
        private readonly string PathImages = Path.Combine(pathRoot, @"\MachineLearning_API\Images\");

        public ImagePredictController(PredictionEnginePool<ModelInput, ModelOutput> predictionEnginePool, IWebHostEnvironment webHostEnvironment)
        {
            this._predictionEnginePool = predictionEnginePool;
            this._webHostEnvironment = webHostEnvironment;
        }

        [HttpPost("Upload")]
        public ActionResult<ModelOutput> Post([FromForm] FileUpload fileUpload)
        {
            try
            {
                if (fileUpload.files.Length > 0)
                {
                    if (!Directory.Exists(PathImages))
                    {
                        Directory.CreateDirectory(PathImages);
                    }
                    using (FileStream fileStream = System.IO.File.Create(Path.Combine(PathImages, fileUpload.files.FileName)))
                    {
                        fileUpload.files.CopyTo(fileStream);
                    }
                    ModelInput sampleData = new ModelInput()
                    {
                        ImageSource = Path.Combine(PathImages, fileUpload.files.FileName),
                    };

                    // Make a single prediction on the sample data and print results
                    var predictionResult = _predictionEnginePool.Predict(sampleData);
                    return Ok(predictionResult);
                }
                else 
                {
                    return BadRequest("Não foi possível fazer upload do arquivo");
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

    }
}
