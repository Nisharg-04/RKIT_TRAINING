using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuiltInMiddlewareDemo.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Admin Get Route");
        }
    }
}
