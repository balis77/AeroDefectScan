using WeatherTrackingSystem.Infrastructure;
using WeatherTrackingSystem.Models;
using WeatherTrackingSystem.Models.Configuration;
using WeatherTrackingSystem.Sensors;

namespace WeatherTrackingSystem;

public class WeatherTracker
{
    private readonly Config _config;
    private readonly MicaSenseHumiditySensor _humiditySensor;
    private readonly Ft205WindSensor _windSensor;
    private readonly Psdk _psdk;
    private readonly Logger _logger;

    public WeatherTracker(Config config)
    {
        _config = config;
        _humiditySensor = new MicaSenseHumiditySensor(config.Bme280);
        _windSensor = new Ft205WindSensor(config.Ft205);
        _psdk = new Psdk(config.Psdk, config.StartCommand, config.CancelCommand);
        _logger = new Logger(config.LogStoragePath);
    }

    public Psdk GetPsdkInstance() => _psdk;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            Console.WriteLine("Weather tracking system started");
            while (cancellationToken.IsCancellationRequested)
            {
                var windData = _windSensor.ReadData();
                await _psdk.SendWindVectorAsync(windData.CalculateWindVector());
                var humidityData = _humiditySensor.ReadData();
                var psdkData = _psdk.TryGetLatestDroneData();
                await _logger.LogAsync(new Log(psdkData, windData, humidityData));
                Thread.Sleep(250);
            }
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Weather tracking system stopped");
        }
        catch (Exception ex)
        {
            ErrorLogger.LogError(ex, _config.LogStoragePath);
        }
    }
}