using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BasicAPI.Controllers
{
    [RoutePrefix("api/routeprefix")]
    public class ItemController : ApiController
    {
        // GET api/<controller>
        [HttpGet]
        [Route("normalGet")]
        public IEnumerable<string> NormalGet()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet]
        [Route("paramGet")]
        public IHttpActionResult Get(int id)
        {
            return Ok("value");
        }
        [HttpGet]
        [Route("queryFilterGet")]
        public string GetByCategory([FromUri] string category)
        {
            return $"value = {category}";
        }

        // POST api/<controller>
        [HttpPost]
        [Route("normalPost")]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/
        [HttpPut]
        [Route("normalPut")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        [Route("normalDelete")]
        public void Delete(int id)
        {
        }
    }
}