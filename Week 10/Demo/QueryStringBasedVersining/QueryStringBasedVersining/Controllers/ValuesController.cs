using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QueryStringBasedVersining.Controllers
{
    [RoutePrefix("api/values")]
    public class ValuesController : ApiController
    {
        [Route("")]
        public IHttpActionResult Get([FromUri]int version)
        {
            if(version == 1)
            {
                return Ok("Version 1");
            }
            else if(version == 2) {
                return Ok("Version 2");
            }
            else
            {
                return BadRequest("No Valid Version Found in query string");
            }

            
        }
    }
}
