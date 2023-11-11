using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.ML;
using static CincoEsesML.QualityModel;

namespace Pages.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly PredictionEnginePool<ModelInput, ModelOutput> _predictionEnginePool;

        public IndexModel(ILogger<IndexModel> logger, PredictionEnginePool<ModelInput, ModelOutput> predictionEnginePool)
        {
            _logger = logger;
            _predictionEnginePool = predictionEnginePool;
        }

        public void OnGet()
        {

        }

        public IActionResult OnGetAnalyzeSentiment(byte[] imageBytes)
        {
            if (imageBytes.Length == 0) return Content("Neutral");
            var input = new ModelInput { ImageSource = imageBytes };
            var prediction = _predictionEnginePool.Predict(input);
            var sentiment = prediction.PredictedLabel == "Seguro" ? "Seguro" : "No Seguro";
            return Content(sentiment);
        }

    }
}