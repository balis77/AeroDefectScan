using System.Text;
using WeatherTrackingSystem.Models;

namespace WeatherTrackingSystem.Infrastructure;

public class Logger
{
    private readonly StreamWriter _writer;
    private bool _headerWritten; 
    
    public Logger(string filePath)
    {
        var fileName = $"INSPECTION_{DateTime.UtcNow:ddmmyyHHmmss}.csv";
        _writer = new StreamWriter(Path.Combine(filePath, fileName), append: true, Encoding.UTF8)
        {
            AutoFlush = true
        };
    }

    private async Task WriteHeader()
    {
        await _writer.WriteLineAsync("Timestamp,CelsiusTemperature,PercentHumidity,BarPressure,WindSpeed,WindDirection,Latitude,Longitude,Altitude,RtkAccuracy,BatteryLevel");
        _headerWritten = true;
    }

    public async Task LogAsync(Log log)
    {
        if (!_headerWritten)
            await WriteHeader();

        string line = string.Join(",",
            log.TimeStamp.ToString("o"),
            log.CelsiusTemperature.ToString("F1"),
            log.PercentHumidity.ToString("F2"),
            log.BarPressure.ToString("F2"),
            log.WindSpeed.ToString("F2"),
            log.WindDirection.ToString("F0"),
            log.Latitude?.ToString("F6") ?? "",
            log.Longitude?.ToString("F6") ?? "",
            log.Altitude?.ToString("F2") ?? "",
            log.RtkAccuracy?.ToString("F3") ?? "",
            log.BatteryLevel?.ToString("F1"));
        
        await _writer.WriteLineAsync(line);
    }
}