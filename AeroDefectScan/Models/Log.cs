using WeatherTrackingSystem.Models.OutputData;

namespace WeatherTrackingSystem.Models;

public class Log
{
    public Log(InputData? psdkData, WindSensorData windData, HumiditySensorData humidityData)
    {
        TimeStamp = DateTime.UtcNow;
        CelsiusTemperature = humidityData.CelsiusTemperature;
        PercentHumidity = humidityData.PercentHumidity;
        BarPressure = humidityData.BarPressure;
        WindSpeed = windData.Speed;
        WindDirection = windData.Direction;

        if (psdkData == default) 
            return;
        
        Latitude = psdkData.Latitude;
        Altitude = psdkData.Altitude;
        Longitude = psdkData.Latitude;
        RtkAccuracy = psdkData.RtkAccuracy;
        BatteryLevel = psdkData.BatteryLevel;
    }

    public DateTime TimeStamp { get; }

    public double CelsiusTemperature { get; }
    
    public double PercentHumidity { get; }

    public double BarPressure { get; }
    
    public double WindSpeed { get; }

    public double WindDirection { get; }

    public double? Latitude { get; }

    public double? Longitude { get; }

    public double? Altitude { get; }

    public double? RtkAccuracy { get; }

    public float? BatteryLevel { get; }

}