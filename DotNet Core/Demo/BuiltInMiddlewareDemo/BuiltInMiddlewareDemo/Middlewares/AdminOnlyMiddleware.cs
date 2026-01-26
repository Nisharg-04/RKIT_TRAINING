using System.Diagnostics;

namespace BuiltInMiddlewareDemo.Middlewares
{
    public class AdminOnlyMiddleware
    {
        public RequestDelegate _next { get; set; }
        public AdminOnlyMiddleware(RequestDelegate next) {
        _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            Debug.WriteLine("Admin middleware started");
            await _next(context);

            Debug.WriteLine("Admin middleware completed");


        }
    }
}
