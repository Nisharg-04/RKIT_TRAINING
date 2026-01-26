using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace BuiltInMiddlewareDemo.Controllers
{
   
        [ApiController]
        [Route("api/demo")]
        public class DemoController : ControllerBase
        {
            [HttpGet("public")]
            public IActionResult Public()
            {
            var res = new
            {
                val = "Pulblic API",
            };
          
                return Ok(res);
            }

          

            [HttpGet("error")]
            public IActionResult Error()
            {
                throw new Exception("Exception thrown");
            }
        }

}
