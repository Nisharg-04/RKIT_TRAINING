using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MiddlewareDemo.Controllers
{
    [ApiController]
  
    public class TestController : ControllerBase
    {
        [HttpGet]
        [Route("api/test/ok")]

        public IActionResult OkEndpoint()
        {
            return Ok("Controller executed successfully");
        }


        [HttpGet]
        [Route("api/test/error")]

        public IActionResult ErrorEndpoint()
        {
            throw new Exception("Something went wrong in controller");
        }
        [HttpGet]
        [Route("blocked")]
        public IActionResult BlockedReq()
        {
            return BadRequest();
        }
    }
}
