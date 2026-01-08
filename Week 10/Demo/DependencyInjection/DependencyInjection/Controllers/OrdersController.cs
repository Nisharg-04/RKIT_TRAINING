using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DependencyInjection.Controllers
{
    using DependencyInjection.Services;
    using System.Web.Http;

    public class OrdersController : ApiController
    {
        private readonly IOrderService _service;


        public OrdersController(IOrderService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("api/orders")]
        public IHttpActionResult Get()
        {
            return Ok(new
            {
                Controller = this.GetHashCode(),
                Service = _service.InstanceId,
                Result = _service.PlaceOrder()
            });
        }
    }

}