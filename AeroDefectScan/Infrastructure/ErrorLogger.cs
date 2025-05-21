namespace WeatherTrackingSystem.Infrastructure;

public class ErrorLogger
{
    private static bool _logCreated = false;
    private static readonly object _lock = new();

    public static void LogError(Exception ex, string path)
    {
        lock (_lock)
        {
            if (_logCreated) return;

            var content = $"[{DateTime.UtcNow:O}] Critical error occurred:\n{ex}\n";
            File.WriteAllText(path, content);
            _logCreated = true;
        }
    }

    public static void Reset() => _logCreated = false;
}