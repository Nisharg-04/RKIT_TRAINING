using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace FiltersDemo.Filters
{
    public class LoggingFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            Debug.WriteLine(" LoggingFilter - Before Action");
        }

        public override void OnActionExecuted(HttpActionExecutedContext context)
        {
            Debug.WriteLine("LoggingFilter - After Action");
        }
    }

}