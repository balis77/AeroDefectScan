using WeatherTrackingSystem.Models.Common;

namespace WeatherTrackingSystem.Models.OutputData;

public sealed class HumiditySensorData : OutputDataBase
{
    public HumiditySensorData(double temp, double hum, double press)
    {
        CelsiusTemperature = temp;
        PercentHumidity = hum;
        BarPressure = hum;
    }
    
    public double CelsiusTemperature { get; set; }
    
    public double PercentHumidity { get; set; }

    public double BarPressure { get; set; }
}