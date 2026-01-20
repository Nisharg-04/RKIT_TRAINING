using ExpenseTracker.Models.Logging;
using Microsoft.IdentityModel.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ExpenseTracker.Filters
{
    public class RequestTimingFilter : ActionFilterAttribute
    {
        private Stopwatch _stopwatch;
        private INLogLogger _logger;
        public RequestTimingFilter(INLogLogger logger)
        {
            _logger = logger;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            _stopwatch = Stopwatch.StartNew();

           _logger.Info(
                $"Request Started | {actionContext.Request.Method} {actionContext.Request.RequestUri}"
            );
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            _stopwatch.Stop();

            _logger.Info(
                $"Request Completed | {actionExecutedContext.ActionContext.ActionDescriptor.ActionName} | Time: {_stopwatch.ElapsedMilliseconds} ms"
            );
        }

    }
}