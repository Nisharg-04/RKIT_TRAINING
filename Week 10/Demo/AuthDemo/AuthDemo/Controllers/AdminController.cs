using AuthDemo.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;

namespace AuthDemo.Controllers
{
    [JwtAuthorize]
    [Authorize(Roles="Admin")]
    [RoutePrefix("api/admin")]
    public class AdminController : ApiController
    {
        [HttpGet]
        [Route("dashboard")]
        public IHttpActionResult Dashboard()
        {
            return Ok("Welcome Admin. This is protected data.");
        }
    }

}