using WeatherTrackingSystem.Models.Common;

namespace WeatherTrackingSystem.Sensors.Contracts;

public interface ISensor<out TDataOutput>
    where TDataOutput : OutputDataBase
{
    TDataOutput ReadData();
}