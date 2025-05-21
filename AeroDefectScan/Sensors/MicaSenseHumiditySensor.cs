using System.Device.I2c;
using Iot.Device.Bmxx80;
using WeatherTrackingSystem.Models.Configuration;
using WeatherTrackingSystem.Models.OutputData;
using WeatherTrackingSystem.Sensors.Contracts;

namespace WeatherTrackingSystem.Sensors;

public class MicaSenseHumiditySensor : ISensor<HumiditySensorData>
{
    private readonly Bme280 _bme280;

    public MicaSenseHumiditySensor(I2CDeviceConfiguration config)
    {
        var i2CDevice = I2cDevice.Create(new I2cConnectionSettings(config.BusId, config.Address));
        _bme280 = new Bme280(i2CDevice);
    }

    public HumiditySensorData ReadData()
    {
        var data =  _bme280.Read();

        return new HumiditySensorData(data.Temperature?.DegreesCelsius ?? default,
            data.Humidity?.Percent ?? default,
            data.Pressure?.Bars ?? default);
    }
}