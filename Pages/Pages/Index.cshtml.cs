using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.ML;
using static CincoEsesML.QualityModel;
using System;
using System.IO;

namespace Pages.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly PredictionEnginePool<ModelInput, ModelOutput> _predictionEnginePool;

        [BindProperty]
        public IFormFile ImageFile { get; set; }

        public string ImagePath { get; set; }

        public string localImagePath { get; set; }

        public string calidad { get; set; }

        public IndexModel(ILogger<IndexModel> logger, PredictionEnginePool<ModelInput, ModelOutput> predictionEnginePool)
        {
            _logger = logger;
            _predictionEnginePool = predictionEnginePool;
        }

        public void OnGet()
        {
            ImagePath = TempData["ImagePath"] as string;
            Console.WriteLine(TempData["ImagePath"]);
            localImagePath = TempData["localImagePath"] as string;
            Console.WriteLine(localImagePath);
            if (localImagePath != null)
            {
                FileByteReader fileReader = new FileByteReader();
                byte[] bytes = fileReader.ReadBytesFromFile(localImagePath);
                if (bytes != null)
                {
                    OnGetAnalyzeSentiment(bytes);
                    calidad = TempData["calidad"] as string;
                    Console.WriteLine(calidad);

                }

            }
        }

        public void OnGetAnalyzeSentiment(byte[] imageBytes)
        {
            if (imageBytes.Length == 0)
            {
                TempData["calidad"] = "Neutral";
                return;
            }

            var input = new ModelInput { ImageSource = imageBytes };
            var prediction = _predictionEnginePool.Predict(input);
            
            var sentiment = prediction.PredictedLabel == "Seguro" ? "Seguro" : "No Seguro";
            TempData["calidad"] = sentiment;

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ImageFile != null && ImageFile.Length > 0)
            {
                // Specify the path where you want to save the uploaded image
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", ImageFile.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                // Optional: Add logic to save the file path to a database or perform other actions
                TempData["localImagePath"] = filePath;
                TempData["ImagePath"] = $"https://localhost:7241/uploads/{ImageFile.FileName}";

                return RedirectToPage("/Index"); // Redirect to another page after successful upload
            }

            ModelState.AddModelError("ImageFile", "Please select a file to upload.");
            return Page();
        }


    }
}