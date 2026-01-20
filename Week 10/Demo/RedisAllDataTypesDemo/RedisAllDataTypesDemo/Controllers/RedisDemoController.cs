using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Configuration;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RedisAllDataTypesDemo.Models;
using StackExchange.Redis;


namespace RedisAllDataTypesDemo.Controllers
{
    
  
    public class RedisDemoController : ApiController
    {
        public enum demoenum{
            first =10,
            second =20,
            third =30
        }
        private readonly IDatabase _db;

        public RedisDemoController()
        {
            _db = RedisManager.Db;
        }

        [HttpGet]
        [Route("api/redis/string")]
        public IHttpActionResult StringDemo()
        {
            _db.StringSet("site:name", "My Web API");
            var value = _db.StringGet("site:name");

            return Ok(value.ToString());
        }

        
        [HttpGet]
        [Route("api/redis/string/json")]
        public IHttpActionResult JsonString()
        {
            var user = new
            {
                Id = "22CP003",
                Name = "Nisharg",
                Rank = demoenum.first
            };

            var settings = new JsonSerializerSettings
            {
                Converters = new List<JsonConverter>
    {
        new StringEnumConverter()
    }
            };
            _db.StringSet("user:1", JsonConvert.SerializeObject(user,settings));
            var value = JsonConvert.DeserializeObject<dynamic>(_db.StringGet("user:1"));
            return Ok(value);
        }
        [HttpGet]
        [Route("api/redis/hash")]
        public IHttpActionResult HashDemo()
        {
            _db.HashSet("student:1", new HashEntry[]
            {
            new HashEntry("Name", "Nisharg"),
            new HashEntry("Age", "21"),
            new HashEntry("Course", ".NET")
            });

            var data = _db.HashGetAll("student:1");

            return Ok(data);
        }


        [HttpGet]
        [Route("api/redis/list")]
        public IHttpActionResult ListDemo()
        {
            _db.ListLeftPush("tasks", "Learn Redis");
            _db.ListLeftPush("tasks", "Learn Web API");

            var items = _db.ListRange("tasks");

            return Ok(items);
        }

        [HttpGet]
        [Route("api/redis/set")]
        public IHttpActionResult SetDemo()
        {
            _db.SetAdd("online:users", "user1");
            _db.SetAdd("online:users", "user2");
            _db.SetAdd("online:users", "user1"); // duplicate ignored

            var users = _db.SetMembers("online:users");

            return Ok(users);
        }

        [HttpGet]
        [Route("api/redis/sortedset")]
        public IHttpActionResult SortedSetDemo()
        {
            _db.SortedSetAdd("leaderboard", "Nisharg", 100);
            _db.SortedSetAdd("leaderboard", "Dakshil", 800);

            var result = _db.SortedSetRangeByRank("leaderboard", order: Order.Descending);

            return Ok(result);
        }


        [HttpGet]
        [Route("api/redis/ttl")]
        public IHttpActionResult TtlDemo()
        {
            _db.StringSet("temp:key", "I will expire",
                expiry: System.TimeSpan.FromSeconds(30));

            return Ok("Key will expire in 30 seconds");
        }
    }

}