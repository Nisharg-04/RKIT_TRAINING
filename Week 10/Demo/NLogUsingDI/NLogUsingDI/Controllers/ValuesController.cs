using NLogUsingDI.Models;
using System;
using System.Web.Http;

namespace NLogFullDemo.Controllers
{
    public class ValuesController : ApiController
    {
        private readonly ILogger _logger;

        public ValuesController(ILogger logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            _logger.Info("GET Values called");
            return Ok("Logging works");
        }

        [HttpGet]
        [Route("api/values/error")]
        public IHttpActionResult Error()
        {
            try
            {
                throw new Exception("Test exception");
            }
            catch (Exception ex)
            {
                _logger.Error("Internal Server Error");
                return InternalServerError();
            }
        }
    }
}
