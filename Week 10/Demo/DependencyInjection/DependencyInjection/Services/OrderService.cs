using DependencyInjection.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DependencyInjection.Repositories;
namespace DependencyInjection.Services
{
    public class OrderService : IOrderService
    {
        public Guid InstanceId { get; } = Guid.NewGuid();

        private readonly IOrderRepository _repository;

        public OrderService(IOrderRepository repository)
        {
            _repository = repository;
        }

        public string PlaceOrder()
        {
            return _repository.GetOrder();
        }
    }

}