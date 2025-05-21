namespace WeatherTrackingSystem.Models.Configuration;

public class Config
{
    public required UartDeviceConfiguration Ft205 { get; set; }

    public required I2CDeviceConfiguration Bme280 { get; set; }

    public required UartDeviceConfiguration Psdk { get; set; }

    public required string LogStoragePath { get; set; }

    public required string CancelCommand { get; set; }
    
    public required string StartCommand { get; set; }
}