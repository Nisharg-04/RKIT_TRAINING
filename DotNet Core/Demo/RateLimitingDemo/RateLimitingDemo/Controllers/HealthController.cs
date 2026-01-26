using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace RateLimitingDemo.Controllers
{
  
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet("health")]
        [DisableRateLimiting]
        public IActionResult HealthCheck()
        {
            return Ok("Healthy");
        }

        [HttpGet("ip")]
        [DisableRateLimiting]
        public IActionResult getClientIp() {
            var ip = HttpContext.Connection.RemoteIpAddress.ToString();
            return Ok(ip);  
        }
    }
}
