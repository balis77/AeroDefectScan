using WeatherTrackingSystem;
using WeatherTrackingSystem.Models.Configuration;

var configuration = new Microsoft.Extensions.Configuration.ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("config.json", optional: false, reloadOnChange: true)
    .Build();

var tracker = new WeatherTracker(configuration.GetSection("config").Get<Config>() ??
                                 throw new ArgumentException("Configuration file not provided!"));


var psdkInstance = tracker.GetPsdkInstance();
var startRequested = new ManualResetEventSlim(false);
var cancelRequested = new ManualResetEventSlim(false);

psdkInstance.CancelRequested += () => { cancelRequested.Set(); };

psdkInstance.StartRequested += () => { startRequested.Set(); };


while (true)
{
    var cts = new CancellationTokenSource();

    startRequested.Wait();
    startRequested.Reset();

    var trackerTask = tracker.StartAsync(cts.Token);

    var cancelTask = Task.Run(() =>
    {
        cancelRequested.Wait();
        cancelRequested.Reset();
        cts.Cancel();
    });

    await Task.WhenAny(trackerTask, cancelTask);
}