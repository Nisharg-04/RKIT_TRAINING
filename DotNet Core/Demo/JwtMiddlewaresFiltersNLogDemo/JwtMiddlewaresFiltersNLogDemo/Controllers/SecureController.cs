using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtMiddlewaresFiltersNLogDemo.Controllers
{
    [ApiController]
    [Route("api/secure")]
    [Authorize] 
    public class SecureController : ControllerBase 
    { 
        [HttpGet("data")] 
        public IActionResult GetData() 
        { 
            return Ok("Secure Data Accessed"); } 
        [HttpGet("error")] 
        public IActionResult ThrowError() 
        { throw new Exception("Test Exception"); 
        } 
    }
}


