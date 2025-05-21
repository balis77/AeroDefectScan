using WeatherTrackingSystem.Models.Common;

namespace WeatherTrackingSystem.Models.Configuration;

public class UartDeviceConfiguration : DeviceConfigurationBase
{
    public required string Port { get; set; }

    public int BaudRate { get; set; }
}