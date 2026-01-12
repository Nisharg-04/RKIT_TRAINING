using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using NLog;
namespace NLogUsingDI
{

    public class WebApiApplication : System.Web.HttpApplication
    {
      

        protected void Application_Start()
        {
            UnityConfig.RegisterComponents();

            LogManager.LoadConfiguration("NLog.config");
            GlobalConfiguration.Configure(WebApiConfig.Register);

        }
    }
}
