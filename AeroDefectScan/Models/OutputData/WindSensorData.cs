using WeatherTrackingSystem.Models.Common;

namespace WeatherTrackingSystem.Models.OutputData;

public sealed class Calculate : OutputDataBase
{
    public static float CalculateCorrosionArea(float partialZx, float partialZy)
    {
        return (float)Math.Sqrt(1 + Math.Pow(partialZx, 2) + Math.Pow(partialZy, 2));
    }

    public static float CalculateHeatingCriticality(float deltaT, float laplacianT, float area, float eta, float K)
    {
        return eta * deltaT * Math.Abs(laplacianT) * area * K;
    }
    public static float FuzzyRiskAssessment(float modelValue, float expertValue, float wModel, float wExpert)
    {
        return wModel * modelValue + wExpert * expertValue;
    }
    public static float EnsemblePrediction(float prob1, float prob2, float gamma)
    {
        return gamma * prob1 + (1 - gamma) * prob2;
    }
    public static float CalculateCrackCriticality(float width, float length, float curvature, float beta, float kappa)
    {
        return beta * width * (1 + kappa) * length;
    }
}
