
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace RateLimitingDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.


            builder.Services.AddControllers();
            builder.Services.AddRateLimiter(options =>
            {
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
                options.AddFixedWindowLimiter("fixed", limiterOptions => {
                    limiterOptions.Window = TimeSpan.FromMinutes(1);
                    limiterOptions.PermitLimit = 10;
                    limiterOptions.QueueLimit = 0;
                });
        //         options.AddPolicy("per-ip", context =>
        // RateLimitPartition.GetFixedWindowLimiter(
        //     partitionKey: context.Connection.RemoteIpAddress?.ToString() ?? "unknown",
        //     factory: key => new FixedWindowRateLimiterOptions
        //     {
        //         PermitLimit = 5,
        //         Window = TimeSpan.FromSeconds(10),
        //         QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
        //         QueueLimit = 0
        //     }));
                //options.AddSlidingWindowLimiter("sliding", opt =>
                //{
                //    opt.Window = TimeSpan.FromMinutes(1);
                //    opt.PermitLimit = 10;
                //    opt.SegmentsPerWindow = 2;
                //});

                options.AddTokenBucketLimiter("token", opt =>
                {
                    opt.TokenLimit = 10;
                    opt.TokensPerPeriod = 1;
                    opt.ReplenishmentPeriod = TimeSpan.FromSeconds(1);
                });

                //options.AddPolicy("role-based", context =>
                //{
                //    var isAdmin = context.User.IsInRole("Admin");

                //    return RateLimitPartition.GetTokenBucketLimiter(
                //        isAdmin ? "admin" : "user",
                //        _ => new TokenBucketRateLimiterOptions
                //        {
                //            TokenLimit = isAdmin ? 100 : 20,
                //            TokensPerPeriod = isAdmin ? 10 : 2,
                //            ReplenishmentPeriod = TimeSpan.FromSeconds(1)
                //        });
                //});
//                 options.AddPolicy("per-user", context =>
// {
//     var userId = context.User.Identity?.IsAuthenticated == true
//         ? context.User.Identity.Name
//         : "anonymous";

//     return RateLimitPartition.GetFixedWindowLimiter(
//         userId,
//         _ => new FixedWindowRateLimiterOptions
//         {
//             PermitLimit = 10,
//             Window = TimeSpan.FromMinutes(1)
//         });
// });


// options.AddPolicy("api-key", context =>
// {
//     var apiKey = context.Request.Headers["X-API-KEY"].FirstOrDefault();

//     return RateLimitPartition.GetFixedWindowLimiter(
//         apiKey ?? "no-key",
//         _ => new FixedWindowRateLimiterOptions
//         {
//             PermitLimit = 100,
//             Window = TimeSpan.FromMinutes(1)
//         });
// });



            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();          
            app.UseRateLimiter();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.MapControllers();

            app.Run();
        }
    }
}