using ExpenseTracker.BAL;
using ExpenseTracker.DAL;
using ExpenseTracker.Filters;
using ExpenseTracker.Models.Logging;
using System.Web.Http;
using Unity;
using Unity.Lifetime;
using Unity.WebApi;

namespace ExpenseTracker
{
    public static class UnityConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();

            // Logger
            container.RegisterSingleton<INLogLogger, NLogLogger>();

            //Auth
            container.RegisterType<IAuthService, AuthService>();
            container.RegisterType<IUserRepository, UserRepository>();

            //Expense
            container.RegisterType<IExpenseService, ExpenseService>();
            container.RegisterType<IExpenseRepository, ExpenseRepository>();


            // Filters
            container.RegisterType<RequestTimingFilter>();
            container.RegisterType<ExceptionFilter>();

            config.DependencyResolver = new UnityDependencyResolver(container);

            // Register filters via Unity
            config.Filters.Add(container.Resolve<RequestTimingFilter>());
            config.Filters.Add(container.Resolve<ExceptionFilter>());
        }
    }

}