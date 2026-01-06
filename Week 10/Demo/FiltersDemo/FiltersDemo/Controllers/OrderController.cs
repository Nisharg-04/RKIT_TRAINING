using FiltersDemo.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FiltersDemo.Controllers
{
    [JwtAuthorize]
    [Authorize(Roles = "Admin")]
    [LoggingFilter]
    [ValidateModelFilter]
    [ApiExceptionFilter]
    [RoutePrefix("api/orders")]
    public class OrdersController : ApiController
    {
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetOrder(int id)
        {
            Console.WriteLine("OrdersController - Action Method");

            if (id == 0)
                throw new Exception("Boom");

            return Ok($"Order {id}");
        }
    }

}