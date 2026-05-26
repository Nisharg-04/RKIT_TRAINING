
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;
using BuiltInMiddlewareDemo.Middlewares;

namespace BuiltInMiddlewareDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Controllers
            builder.Services.AddControllers();

            // Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyCorsPolicy", policy =>
                {
                    policy
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });

                options.AddPolicy("localhost", policy =>
                {
                    policy.WithOrigins("http://127.0.0.1:5508")
                    .AllowAnyHeader()
                    .AllowAnyMethod();

                });
            });

            builder.Services.AddRateLimiter(options =>
            {   
                options.AddFixedWindowLimiter("fixed", opt =>
                {
                    opt.Window = TimeSpan.FromSeconds(10);
                    opt.PermitLimit = 3;
                    opt.QueueLimit = 0;
                });
            });

            var app = builder.Build();

            app.UseWhen(context =>
            {
                return context.Request.Path.StartsWithSegments("/api/admin");
            },
            branch =>
            {
                branch.UseMiddleware<AdminOnlyMiddleware>();
            });



            //Exception handling
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
            app.UseHsts();
            }

            //  Swagger
            app.UseSwagger();
            app.UseSwaggerUI();

            //  HTTPS redirect
            app.UseHttpsRedirection();

            // Static files
            app.UseStaticFiles();

            // Routing //option after .net 6 implecity injected by 
            //app.UseRouting();

            // CORS
            app.UseCors("localhost");


            // Rate limiting
            app.UseRateLimiter();

            // Endpoint mapping
            app.MapControllers().RequireRateLimiting("fixed");

            // Terminal
            app.Run();

        }
    }
}