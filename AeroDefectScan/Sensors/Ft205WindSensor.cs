using System.IO.Ports;
using WeatherTrackingSystem.Models.Configuration;
using WeatherTrackingSystem.Models.OutputData;
using WeatherTrackingSystem.Sensors.Contracts;

namespace WeatherTrackingSystem.Sensors;

public class Ft205WindSensor : ISensor<WindSensorData>
{
    private readonly SerialPort _serialPort;
    
    public Ft205WindSensor(UartDeviceConfiguration config)
    {
        _serialPort = new SerialPort(config.Port, config.BaudRate);
        _serialPort.Open();
    }

    public WindSensorData ReadData() => new(ReadWindSpeed(), ReadAzimuth());
    
    private double ReadWindSpeed()
    {
        string line = _serialPort.ReadLine();
        if (line.StartsWith("WS:"))
        {
            string speedStr = line.Replace("WS:", "");
            if (double.TryParse(speedStr, out double speed))
                return speed;
        }
        return default;
    }

    private double ReadAzimuth()
    {
        string line = _serialPort.ReadLine();
        if (line.StartsWith("AZ:"))
        {
            string azStr = line.Replace("AZ:", "");
            if (double.TryParse(azStr, out double azimuthDeg))
                return azimuthDeg * Math.PI / 180.0;
        }

        return default;
    }
}