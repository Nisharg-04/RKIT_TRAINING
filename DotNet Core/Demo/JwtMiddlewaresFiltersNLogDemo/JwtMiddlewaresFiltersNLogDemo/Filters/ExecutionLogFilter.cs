using Microsoft.AspNetCore.Mvc.Filters;

namespace JwtMiddlewaresFiltersNLogDemo.Filters
{
    public class ExecutionLogFilter : IActionFilter
    {
        private readonly ILogger<ExecutionLogFilter> _logger;


        public ExecutionLogFilter(ILogger<ExecutionLogFilter> logger)
        {
            _logger = logger;
        }


        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("[Filter] Before Controller Action");
        }


        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("[Filter] After Controller Action");
        }
    }
}
