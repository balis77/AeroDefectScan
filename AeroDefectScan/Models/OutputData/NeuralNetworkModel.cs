namespace WeatherTrackingSystem.Models.OutputData;

class NeuralNetworkModel
{
    public float PredictProbability(ImageData image, int x, int y) => Random.Shared.NextSingle();
}