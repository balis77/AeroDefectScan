namespace WeatherTrackingSystem.Models;

public class WindVector
{
    public WindVector(double x, double y, double z)
    {
        XAxis = x;
        YAxis = y;
        ZAxis = z;
    }

    public WindVector(double x, double y)
    {
        XAxis = x;
        YAxis = y;
        ZAxis = 0;
    }

    public double XAxis { get; }

    public double YAxis { get; }

    public double ZAxis { get; }
}