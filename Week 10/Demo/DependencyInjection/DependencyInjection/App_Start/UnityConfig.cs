using DependencyInjection.Logging;
using DependencyInjection.Repositories;
using DependencyInjection.Services;
using System.Web.Http;
using Unity;
using Unity.Lifetime;
using Unity.WebApi;

namespace DependencyInjection
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<ILogger, ConsoleLogger>(
              new ContainerControlledLifetimeManager());

            container.RegisterType<IOrderRepository, OrderRepository>(
                new HierarchicalLifetimeManager());

            container.RegisterType<IOrderService, OrderService>(
                new TransientLifetimeManager());

      

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}