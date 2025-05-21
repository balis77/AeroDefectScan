using WeatherTrackingSystem.Models.Common;

namespace WeatherTrackingSystem.Models.Configuration;

public class I2CDeviceConfiguration : DeviceConfigurationBase
{
    public int BusId { get; set; }

    public int Address { get; set; }
}