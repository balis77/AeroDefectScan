using WeatherTrackingSystem.Models.Common;

namespace WeatherTrackingSystem.Models.OutputData;

class Defect
{
    public string Type;
    public float Width;
    public float Length;
    public float Area;
    public float DeltaTemperature;
    public float Curvature;
    public float FinalCriticality;
}