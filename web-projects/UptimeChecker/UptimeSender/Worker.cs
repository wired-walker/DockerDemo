namespace UptimeSender
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    if (_logger.IsEnabled(LogLevel.Information))
                    {
                        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                    }

                    var handler = new HttpClientHandler();
                    handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

                    using (var client = new HttpClient(handler))
                    {
                        var resp = await client.PostAsync("http://localhost:8080/Uptime", null);
                        if (resp.StatusCode != System.Net.HttpStatusCode.OK)
                        {
                            _logger.LogError($"Failed to checkin. Response: {resp.Content}");
                        }
                    }
                    await Task.Delay(1000, stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.ToString());

                    await Task.Delay(1000, stoppingToken);
                }
            }
        }
    }
}
