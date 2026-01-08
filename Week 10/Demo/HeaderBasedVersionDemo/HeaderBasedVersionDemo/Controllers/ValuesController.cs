using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HeaderBasedVersionDemo.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IHttpActionResult Get()
        {
            var version = Request.Headers.Contains("X-API-Version") ? Request.Headers.GetValues("X-API-Version").FirstOrDefault():"0";
            if(version == "1")
            {
                return Ok("Version 1");
            }
            else
            {
                return BadRequest("Versiion Mismatch");
            }
        }
    }
}
