using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace UptimeReceiver.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UptimeController : ControllerBase
    {

        private readonly ILogger<UptimeController> _logger;

        public UptimeController(ILogger<UptimeController> logger)
        {
            _logger = logger;
        }

        [HttpPost("Checkin")]
        public bool Checkin()
        {
            using (var redisConn = ConnectionMultiplexer.Connect("redis:6379"))
            {
                var db = redisConn.GetDatabase();
                db.StringSet("lastCheckin", DateTime.UtcNow.ToString());
            }
            return true;
        }
    }
}
