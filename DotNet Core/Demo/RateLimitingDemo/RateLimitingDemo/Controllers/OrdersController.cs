using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace RateLimitingDemo.Controllers
{
    [EnableRateLimiting("token")]
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetOrders()
        {
            return Ok("Orders");
        }

        [HttpPost]
        public IActionResult CreateOrder()
        {
            return Ok("Created");
        }
    }

}
