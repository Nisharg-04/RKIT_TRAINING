public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            Console.WriteLine("GLOBAL EXCEPTION MIDDLEWARE: Enter");
            await _next(context);
        }
        catch (Exception ex)
        {
            Console.WriteLine("GLOBAL EXCEPTION MIDDLEWARE: Caught Exception");
            Console.WriteLine(ex.Message);

            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("Handled by Global Exception Middleware");
        }
    }
}
