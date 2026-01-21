using RegenerateTokenDemo.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace RegenerateTokenDemo.Controllers
{
    [JwtAuthorize]
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok(new[] { "Laptop", "Phone", "Tablet" });
        }
    }

}