using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace JwtMiddlewaresFiltersNLogDemo.Filters
{
    public class ActionTimerFilter : IActionFilter

    {
        private Stopwatch _watch;
        private readonly ILogger<ActionTimerFilter> _logger;


        public ActionTimerFilter(ILogger<ActionTimerFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _watch.Stop();
            string controllerName = context.ActionDescriptor.RouteValues["controller"];
            string actionName = context.ActionDescriptor.RouteValues["action"];
           _logger.LogInformation($"[Filter] Contoller {controllerName} Action {actionName} took {_watch.ElapsedMilliseconds} ms");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _watch = Stopwatch.StartNew();
        }
    }
}
