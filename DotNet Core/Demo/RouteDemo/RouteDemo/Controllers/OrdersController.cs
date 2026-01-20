using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RouteDemo.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        [HttpGet("{date:datetime}")]
        public IActionResult ByDate(DateTime date)
        {
            return Ok(date);
        }


        [HttpGet("{id:int}")]
        public IActionResult ById(int id)
        {
            return Ok(id);
        }
    }

}
