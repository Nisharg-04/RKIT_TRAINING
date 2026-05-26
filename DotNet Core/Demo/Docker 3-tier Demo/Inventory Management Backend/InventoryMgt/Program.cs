
using InventoryMgt.BAL;
using InventoryMgt.BAL.Interfaces;
using InventoryMgt.DAL;
using InventoryMgt.DAL.Interfaces;
using InventoryMgt.Middleware;
using InventoryMgt.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NLog.Web;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Text;
using NLog;
using Microsoft.OpenApi.Models;

namespace InventoryMgt
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
            var builder = WebApplication.CreateBuilder(args);
            //required for nlog
            builder.Logging.ClearProviders();
            builder.Host.UseNLog();
            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddSingleton<IDbConnectionFactory>(
    new OrmLiteConnectionFactory(
            builder.Configuration.GetConnectionString("MySql"),
        MySqlDialect.Provider
    ));
            builder.Services.AddScoped<IUserRepository, UserContext>();
            builder.Services.AddScoped<IUserService, UserHandler>();

            builder.Services.AddScoped<IProductRepository, ProductContext>();
            builder.Services.AddScoped<IProductService, ProductHandler>();

            builder.Services.AddSingleton<JwtTokenGenerator>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>

            {

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Inventory API",
                    Version = "v1"
                });
                


                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter: Bearer {your JWT token}"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
                        )
                    };
                });
            builder.Services.AddAuthorization();

            var app = builder.Build();
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseMiddleware<RequestLoggingMiddleware>();
            // Configure the HTTP request pipeline.
           
           
                app.UseSwagger();
                app.UseSwaggerUI();
           

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}