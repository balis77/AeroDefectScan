using WeatherTrackingSystem.Models.OutputData;

void MainFlow()
{
    var image = MergeSpectralImages(rgb: new float[100, 100], ir: new float[100, 100], alpha: 0.8f, beta: 0.2f);

    var model1 = new NeuralNetworkModel();
    var model2 = new NeuralNetworkModel();

    float p1 = model1.PredictProbability(image, 50, 50);
    float p2 = model2.PredictProbability(image, 50, 50);

    float pEnsemble = Calculate.EnsemblePrediction(p1, p2, gamma: 0.6f);
    Console.WriteLine($"Ймовірність дефекту: {pEnsemble}");

    var defect = new Defect
    {
        Type = "crack",
        Width = 0.01f,
        Length = 0.2f,
        Curvature = 0.05f
    };

    float crackCrit = Calculate.CalculateCrackCriticality(defect.Width, defect.Length, defect.Curvature, beta: 1.0f, kappa: 0.2f);
    float fuzzy = Calculate.FuzzyRiskAssessment(crackCrit, expertValue: 4.5f, wModel: 0.6f, wExpert: 0.4f);

    defect.FinalCriticality = fuzzy;
    Console.WriteLine($"Оцінка критичності: {defect.FinalCriticality:F2}");
}



ImageData MergeSpectralImages(float[,] rgb, float[,] ir, float alpha, float beta)
{
    int width = rgb.GetLength(0);
    int height = rgb.GetLength(1);
    float[,] merged = new float[width, height];

    for (int x = 0; x < width; x++)
    {
        for (int y = 0; y < height; y++)
        {
            float gamma = rgb[x, y] - alpha * ir[x, y] - beta * ir[x, y];
            merged[x, y] = gamma;
        }
    }

    return new ImageData { RgbData = rgb, IrData = ir };
}