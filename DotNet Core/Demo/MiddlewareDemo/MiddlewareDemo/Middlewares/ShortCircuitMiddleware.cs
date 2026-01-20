namespace MiddlewareDemo.Middlewares
{
    public class ShortCircuitMiddleware
    {
        private readonly RequestDelegate _next;


        public ShortCircuitMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/blocked"))
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Request blocked by middleware");
                Console.WriteLine("Request Blocked");
                return; //Pipeline stops here
            }


            await _next(context);
        }
    }
}
