using LoggingDemo.Models;
using LoggingDemo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoggingDemo.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;


        private readonly OrderService _service;

        public OrdersController(
            ILogger<OrdersController> logger,
            OrderService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost]
        public IActionResult Create(OrderRequest request)
        {
            _logger.LogInformation(
                "Received order {OrderId} for user {UserId}",
                request.OrderId,
                request.UserId);

            _service.ProcessOrder(request);

            return Ok();
        }
        [HttpGet]
        public IActionResult Get()
        {
            throw new Exception("Error in Controller");
        }

            }

}
