using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RouteDemo.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok("All users");
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            return Ok($"User with id {id}");
        }

        [HttpGet("name/{name:minlength(3)}")]
        public IActionResult GetByName(string name)
        {
            return Ok($"User name {name}");
        }

        // ROUTE PRIORITY DEMO
        // GET /api/users/me
        [HttpGet("me")]
        public IActionResult Me()
        {
            return Ok("Current user");
        }
    }

}
