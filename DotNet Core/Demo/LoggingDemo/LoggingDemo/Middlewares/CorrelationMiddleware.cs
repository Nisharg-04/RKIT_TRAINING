namespace LoggingDemo.Middlewares
{
    public class CorrelationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CorrelationMiddleware> _logger;

        public CorrelationMiddleware(
            RequestDelegate next,
            ILogger<CorrelationMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var correlationId = Guid.NewGuid().ToString();

            using (_logger.BeginScope(new Dictionary<string, object>
            {
                ["CorrelationId"] = correlationId,
                ["RequestPath"] = context.Request.Path,
                ["HttpMethod"] = context.Request.Method
            }))
            {
                _logger.LogInformation("Request started");

                await _next(context);

                _logger.LogInformation("Request finished");
            }
        }
    }

}
