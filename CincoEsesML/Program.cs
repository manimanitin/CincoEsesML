//Load sample data
using CincoEsesML;

string _assetsPath = Path.Combine(Environment.CurrentDirectory, "assets");
var imageBytes = File.ReadAllBytes($"{_assetsPath}/cavi.jpg");
QualityModel.ModelInput sampleData = new QualityModel.ModelInput()
{
    ImageSource = imageBytes,
};

//Load model and predict output
var result = QualityModel.Predict(sampleData);

Console.WriteLine("\n" + result.PredictedLabel);

