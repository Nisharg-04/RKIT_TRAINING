using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using NLog;

namespace NLogDemo
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private static readonly Logger Logger =
            LogManager.GetCurrentClassLogger();
        protected void Application_Start()
        {
            
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Logger.Info("Application started");

        }
        protected void Application_End()
        {
            Logger.Info("Application stopped");
            LogManager.Shutdown();
        }
    }
}
