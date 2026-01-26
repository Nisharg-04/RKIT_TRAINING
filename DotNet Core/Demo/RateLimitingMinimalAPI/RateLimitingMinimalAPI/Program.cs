
using Microsoft.AspNetCore.RateLimiting;

namespace RateLimitingMinimalAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddRateLimiter(options =>
            {
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
                options.AddFixedWindowLimiter("fixed", limiterOptions => {
                    limiterOptions.Window = TimeSpan.FromMinutes(1);
                    limiterOptions.PermitLimit = 10;
                    limiterOptions.QueueLimit = 0;
                });
                //options.AddSlidingWindowLimiter("sliding", opt =>
                //{
                //    opt.Window = TimeSpan.FromMinutes(1);
                //    opt.PermitLimit = 10;
                //    opt.SegmentsPerWindow = 2;
                //});

                //options.AddTokenBucketLimiter("token", opt =>
                //{
                //    opt.TokenLimit = 10;
                //    opt.TokensPerPeriod = 1;
                //    opt.ReplenishmentPeriod = TimeSpan.FromSeconds(1);
                //});



            });
          
            var app = builder.Build();
         
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseRateLimiter();
            app.UseAuthorization();

            app.MapGet("/", () =>
            {
                    return Results.Ok("Welcome To RKIT");
            }).RequireRateLimiting("fixed");

            app.Run();
        }
    }
}