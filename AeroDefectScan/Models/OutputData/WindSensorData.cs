using WeatherTrackingSystem.Models.Common;

namespace WeatherTrackingSystem.Models.OutputData;

public sealed class WindSensorData : OutputDataBase
{
    public WindSensorData(double speed, double direction)
    {
        Speed = speed;
        Direction = direction;
    }
    public double Speed { get; }

    public double Direction { get; }

    public WindVector CalculateWindVector()
    {
        double x = Speed * Math.Cos(Direction);
        double y = Speed * Math.Sin(Direction);

        return new WindVector(x, y);
    }
}