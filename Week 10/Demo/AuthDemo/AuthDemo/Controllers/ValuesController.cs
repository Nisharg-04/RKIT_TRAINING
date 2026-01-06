using AuthDemo.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AuthDemo.Controllers
{
    [JwtAuthorize]
    [RoutePrefix("api/values")]
    public class ValuesController : ApiController
    {
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            var username = User.Identity.Name;
            return Ok($"Hello {username}, you are authenticated");
        }
    }




}