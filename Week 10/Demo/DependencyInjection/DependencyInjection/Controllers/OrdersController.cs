using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DependencyInjection.Controllers
{
    using DependencyInjection.Logging;
    using DependencyInjection.Services;
    using System.Web.Http;

    public class OrdersController : ApiController
    {
        private readonly IOrderService _service;
        private ILogger _logger;

        public OrdersController(IOrderService service,ILogger logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        [Route("api/orders")]
        public IHttpActionResult Get()
        {
            _logger.Log("Callaing Place Order from controller");
            return Ok(new
            {
                Controller = this.GetHashCode(),
                Service = _service.InstanceId,
                Result = _service.PlaceOrder()
            });
        }
    }

}