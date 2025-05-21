namespace WeatherTrackingSystem.Models;

public class InputData
{
    public double Latitude { get; set; }
    
    public double Longitude { get; set; }
    
    public double Altitude { get; set; }
    
    public double RtkAccuracy { get; set; }
    
    public float BatteryLevel { get; set; }
}