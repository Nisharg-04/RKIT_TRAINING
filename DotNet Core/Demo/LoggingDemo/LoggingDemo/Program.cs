
using LoggingDemo.Middlewares;
using LoggingDemo.Repositories;
using LoggingDemo.Services;
using NLog.Web;

namespace LoggingDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Logging.ClearProviders();
            //builder.Logging.AddConsole(o => o.IncludeScopes = true);
            //builder.Logging.AddJsonConsole(o => o.IncludeScopes = true);
            //builder.Logging.AddDebug();


            //for nlog
            builder.Host.UseNLog();
            // Add services to the container.

            builder.Services.AddControllers();
      
            builder.Services.AddScoped<OrderService>();
            builder.Services.AddScoped<OrderRepository>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            app.UseMiddleware<CorrelationMiddleware>();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}