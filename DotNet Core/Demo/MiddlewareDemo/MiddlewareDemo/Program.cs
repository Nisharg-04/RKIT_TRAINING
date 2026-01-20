using MiddlewareDemo.Middlewares;
using MiddlewareDemo.Controllers;
namespace MiddlewareDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            var app = builder.Build();
            app.UseMiddleware<GlobalExceptionMiddleware>();
            app.Use(async (context, next) =>
            {
                Console.WriteLine("INLINE MIDDLEWARE: Before Controller");
                await next();
                Console.WriteLine("INLINE MIDDLEWARE: After Controller");
            });
            app.UseMiddleware<ShortCircuitMiddleware>();
         
            app.UseMiddleware<RequestLoggingMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseRouting();


            app.MapControllers();

            app.Run();
        }
    }
}