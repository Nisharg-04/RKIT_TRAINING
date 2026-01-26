using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace FiltersDemo.Filters
{

    public class ApiKeyAuthorizationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue("X-API-KEY", out var key)
                || key != "secret123")
            {
                context.Result = new UnauthorizedObjectResult("Invalid API Key");
            }
        }
    }

}
