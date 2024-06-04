using StackExchange.Redis;

namespace UptimeMonitor
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ConnectionMultiplexer _redisConn;
        private readonly int _delaySeconds;
        private bool _up;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            _redisConn = ConnectionMultiplexer.Connect("redis:6379");
            _up = true;
            _redisConn.GetDatabase().StringSet("lastCheckin", DateTime.MinValue.ToString());
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var notifier = new DiscordNotifier();

                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                if (_redisConn != null && _redisConn.IsConnected)
                {

                    var lastCheckinString = _redisConn.GetDatabase().StringGet("lastCheckin").ToString();

                    if (String.IsNullOrEmpty(lastCheckinString))
                    {
                        _logger.LogInformation("LastCheckin value does not exist.");
                        await Task.Delay(1000, stoppingToken);
                        continue;
                    }


                    var lastCheckin = DateTime.Parse(lastCheckinString);
                    var now = DateTime.UtcNow;
                    if (now > lastCheckin + TimeSpan.FromSeconds(30) && _up) 
                    {
                        _logger.LogInformation($"DOWN: LastCheckin is out of date! Now: {now}, LastCheckin: {lastCheckin}");
                        //Gmailer.SendTextMessage("Internet is down!");
                        _up = false;   
                    }
                    else if (!_up && lastCheckin >= DateTime.UtcNow - TimeSpan.FromSeconds(30))
                    {
                        _logger.LogInformation($"UP: LastCheckin is recent: {lastCheckin}");
                        //Gmailer.SendTextMessage("Internet is back up!");
                        _up = true;
                    }
                }

               


                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
