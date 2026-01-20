using ExpenseTracker.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using NLog;
namespace ExpenseTracker
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var config = GlobalConfiguration.Configuration;

            UnityConfig.Register(config);
            WebApiConfig.Register(config);

            config.EnsureInitialized();
            OrmLiteConfig.Register();

        }
    }
}
