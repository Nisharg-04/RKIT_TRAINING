using LoggingDemo.Models;
using LoggingDemo.Repositories;

namespace LoggingDemo.Services
{
    public class OrderService
    {
        private readonly ILogger<OrderService> _logger;
        private readonly OrderRepository _repo;

        public OrderService(
            ILogger<OrderService> logger,
            OrderRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        public void ProcessOrder(OrderRequest order)
        {
            using (_logger.BeginScope(new Dictionary<string, object>
            {
                ["Service"] = "OrderService"
            }))
            {
                _logger.LogInformation(
                    "Processing order {OrderId} amount {Amount}",
                    order.OrderId,
                    order.Amount);

                _logger.LogTrace("This is TRACE log - very detailed debugging");

                _logger.LogDebug("This is DEBUG log - debugging variable values");

                _logger.LogInformation("This is INFO log - normal app flow");

                _logger.LogWarning("This is WARNING log - something unusual happened");
                _logger.LogCritical("This is CRITICAL log - system level failure");




                if (order.Amount > 10000)
                {
                    _logger.LogWarning(
                        "High value order detected {OrderId}",
                        order.OrderId);
                }

                _repo.Save(order);
            }
        }
    }

}
