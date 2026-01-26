using Microsoft.AspNetCore.Mvc.Filters;

namespace FiltersDemo.Filters
{
    public class ResponseHeaderFilter : IResultFilter
    {
        public void OnResultExecuting(ResultExecutingContext context)
        {
            context.HttpContext.Response.Headers.Add("X-App", "Demo");
        }

        public void OnResultExecuted(ResultExecutedContext context) { }
    }

}
