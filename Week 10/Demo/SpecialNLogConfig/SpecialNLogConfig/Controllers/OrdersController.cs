using SpecialNLogConfig.Global_Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace SpecialNLogConfig.Controllers
{
   

    public class OrdersController : ApiController
    {
        private readonly IAppLogger _logger;


        public OrdersController(IAppLogger logger)
        {
            _logger = logger;
         
        }

        [HttpGet]
        [Route("api/orders")]
        public IHttpActionResult Get()
        {
            _logger.Info("Orders API called");

            try
            {
                throw new Exception("Test exception");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in Orders API");
            }

            return Ok(new { Message = "Orders returned" });
        }
    }

}