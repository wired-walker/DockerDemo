using Microsoft.AspNetCore.Mvc;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RedisController : Controller
    {
        [HttpGet]
        public RedisResult GetFoo()
        {

            var mgr = new RedisManagerPool("DESKTOP-UE0LGGV:6379");

            var fooValue = string.Empty;
            using (var client = mgr.GetClient())
            {
                client.Set("foo", "bar");
                fooValue = client.Get<string>("foo");

            }
            return new RedisResult() { Key = "foo", Value = fooValue };
        }
    }

    public class RedisResult
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
