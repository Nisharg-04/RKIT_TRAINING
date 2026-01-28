using LoggingDemo.Models;

namespace LoggingDemo.Repositories
{
    public class OrderRepository
    {
        private readonly ILogger<OrderRepository> _logger;

        public OrderRepository(ILogger<OrderRepository> logger)
        {
            _logger = logger;
        }

        public void Save(OrderRequest order)
        {
            _logger.LogInformation(
                "Saving order {OrderId} to database",
                order.OrderId);
        }
    }

}
