using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
namespace HTTPCachingDemo.Controllers

    //  use this in browser dev tools 
  //  fetch("/api/cache/public")
  //.then(r => r.json())
  //.then(console.log);

{
    [RoutePrefix("api/cache")]
    public class PublicCacheController : ApiController
    {
        [HttpGet]
        [Route("public")]
        public IHttpActionResult PublicData()
        {
            var data = new
            {
                Message = "Public data",
                Time = DateTime.UtcNow
            };

            var response = Request.CreateResponse(HttpStatusCode.OK, data);

            response.Headers.CacheControl = new CacheControlHeaderValue
            {
                Public = true,
                MaxAge = TimeSpan.FromSeconds(30)
            };

            return ResponseMessage(response);

        }

    }
}