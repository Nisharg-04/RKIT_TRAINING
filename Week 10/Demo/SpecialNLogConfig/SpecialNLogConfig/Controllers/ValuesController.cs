using SpecialNLogConfig.Special_Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SpecialNLogConfig.Controllers
{
    public class ValuesController : ApiController
    {
        private readonly ISpecialLogger _specialLogger;
        // GET api/<controller>
        public ValuesController(ISpecialLogger logger)
        {
            _specialLogger = logger;

        }
        public IEnumerable<string> Get()
        {
            _specialLogger.Audit("get req in special l0ogger");
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}