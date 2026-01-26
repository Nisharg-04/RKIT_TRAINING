using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace FiltersDemo.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.Result = new ObjectResult(new
            {
                message = "Something went wrong",
                detail = context.Exception.Message
            })
            {
                StatusCode = 500
            };
        }
    }
}
