using System.Diagnostics;

namespace ProductsMinimalAPIWithVaidation.Filters
{
    public class LoggingFilter : IEndpointFilter

    {
        public async  ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        { 
            Console.WriteLine("Before endpoint");

            var result = await next(context);

            Console.WriteLine("After endpoint");

            return result;
        }
    }
}
