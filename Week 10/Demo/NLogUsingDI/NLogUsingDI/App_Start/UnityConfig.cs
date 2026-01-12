using NLogUsingDI.Models;
using System.Web.Http;
using Unity;
using Unity.Lifetime;
using Unity.WebApi;

namespace NLogUsingDI
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<ILogger, NLogLogger>(
              new ContainerControlledLifetimeManager());

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}