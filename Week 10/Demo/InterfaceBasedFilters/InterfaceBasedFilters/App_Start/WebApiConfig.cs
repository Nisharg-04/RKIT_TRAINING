using InterfaceBasedFilters.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace InterfaceBasedFilters
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // add al the filters here to make it glolal 
            config.Filters.Add(new SimpleAuthenticationFilter());
            config.Filters.Add(new SimpleAuthorizationFilter());
            config.Filters.Add(new GlobalExceptionFilter());
            config.Filters.Add(new LoggingActionFilter());
            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
