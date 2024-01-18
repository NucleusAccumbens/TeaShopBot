namespace TeaShopBotWeb;

public class TimeHostedService : IHostedService, IDisposable
{
    private int _executionCount = 0;

    private readonly ILogger<TimeHostedService> _logger;

    private Timer? _timer = null;


    public TimeHostedService(ILogger<TimeHostedService> logger)
    {
        _logger = logger;
    }

    public Task StartAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Timed Hosted Service running.");

        _timer = new Timer(GetUpdates, null, TimeSpan.Zero,
            TimeSpan.FromMinutes(5));

        return Task.CompletedTask;
    }

    private void GetUpdates(object? state)
    {
        var count = Interlocked.Increment(ref _executionCount);

        _logger.LogInformation(
            "Timed Hosted Service is working. Count: {Count}", count);

        PingSite();
    }

    public Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Timed Hosted Service is stopping.");

        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }

    private void PingSite()
    {
        try
        {
            var client = new HttpClient();

            var res = client.GetAsync("https://noncredistka.bsite.net/").Result;

            Console.WriteLine($"{res.Content.Headers}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }
}
