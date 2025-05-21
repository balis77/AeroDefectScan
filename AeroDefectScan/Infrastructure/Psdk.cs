using System.IO.Ports;
using System.Text;
using System.Text.Json;
using WeatherTrackingSystem.Models;
using WeatherTrackingSystem.Models.Configuration;

namespace WeatherTrackingSystem.Infrastructure;

public class Psdk : IDisposable
{
    private readonly SerialPort _port;
    private readonly List<string> _receivedData;
    private readonly string _cancelCommand;
    private readonly string _startCommand;

    public event Action CancelRequested;
    public event Action StartRequested;

    public Psdk(UartDeviceConfiguration configuration, string startCommand, string cancelCommand)
    {
        _startCommand = startCommand;
        _cancelCommand = cancelCommand;
        _port = new SerialPort(configuration.Port, configuration.BaudRate)
        {
            Encoding = Encoding.UTF8,
            NewLine = "\n",
            ReadTimeout = 500,
            WriteTimeout = 500
        };
        _receivedData = [];
        _port.DataReceived += OnDataReceived;
        _port.Open();
    }

    private void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
    {
        var incoming = _port.ReadExisting();
        if (!string.IsNullOrEmpty(incoming))
        {
            switch (incoming)
            {
                case var _ when incoming == _cancelCommand:
                    CancelRequested.Invoke();
                    break;

                case var _ when incoming == _startCommand:
                    StartRequested.Invoke();
                    break;

                default:
                    _receivedData.Add(incoming);
                    break;
            }
        }
    }

    public async Task SendWindVectorAsync(WindVector vector)
    {
        var json = JsonSerializer.Serialize(vector);
        await _port.BaseStream.WriteAsync(Encoding.UTF8.GetBytes(json + "\n"));
        await _port.BaseStream.FlushAsync();
    }

    public InputData? TryGetLatestDroneData()
    {
        var raw = _receivedData.LastOrDefault();

        try
        {
            var lastLine = raw?.Split('\n', StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
            if (string.IsNullOrWhiteSpace(lastLine)) return null;
            return JsonSerializer.Deserialize<InputData>(lastLine);
        }
        catch
        {
            return null;
        }
    }


    public void Dispose()
    {
        if (_port.IsOpen)
        {
            _port.DataReceived -= OnDataReceived;
            _port.Close();
        }
    }
}