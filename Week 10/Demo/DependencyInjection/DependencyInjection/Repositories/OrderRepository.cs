using DependencyInjection.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DependencyInjection.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public Guid InstanceId { get; } = Guid.NewGuid();

        private readonly ILogger _logger;
        public OrderRepository(ILogger logger)
        {
            _logger = logger;
        }

        public string GetOrder()
        {
            _logger.Log("Fetching order from DB");
            return "Order-123";
        }
    }

}