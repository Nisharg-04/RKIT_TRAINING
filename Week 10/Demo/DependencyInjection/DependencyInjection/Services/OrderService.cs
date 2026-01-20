using DependencyInjection.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DependencyInjection.Repositories;
using DependencyInjection.Logging;

namespace DependencyInjection.Services
{
    public class OrderService : IOrderService
    {
        public Guid InstanceId { get; } = Guid.NewGuid();

        private readonly IOrderRepository _repository;
        private  ILogger _logger;

        public OrderService(IOrderRepository repository,ILogger logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public string PlaceOrder()
        {
            _logger.Log("Placing Order from service");

            return _repository.GetOrder();
        }
    }

}