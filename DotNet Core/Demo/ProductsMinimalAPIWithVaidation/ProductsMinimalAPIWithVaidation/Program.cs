
using Microsoft.AspNetCore.Http.HttpResults;
using ProductsMinimalAPIWithVaidation.Filters;
using ProductsMinimalAPIWithVaidation.Models;
using ProductsMinimalAPIWithVaidation.Services;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace ProductsMinimalAPIWithVaidation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //to add validation
            builder.Services.AddProblemDetails();

            var app = builder.Build();
            // Configure the HTTP request pipeline.
            
                app.UseSwagger();
                app.UseSwaggerUI();
            
            app.MapGet("api/products", () => {
                return Results.Ok(ProductService.GetAllProducts());
            }).AddEndpointFilter<LoggingFilter>(); 

            app.MapGet("api/products/{id:int}", (int id) => {
                var product = ProductService.GetProductById(id);
                return product is null
                    ? Results.NotFound()
                    : Results.Ok(product);
            }).AddEndpointFilter<LoggingFilter>();

            app.MapPost("api/products", (Product product) => {
                var validationResults = new List<ValidationResult>();
                var context = new ValidationContext(product);

                if (!Validator.TryValidateObject(product, context, validationResults, true))
                {
                    
                    var errors = validationResults
                        .GroupBy(e => e.MemberNames.FirstOrDefault() ?? "")
                        .ToDictionary(
                            g => g.Key,
                            g => g.Select(e => e.ErrorMessage!).ToArray()
                        );
                    
                    return Results.ValidationProblem(errors);
                }
                var created = ProductService.AddProduct(product);
                return Results.Created($"/api/products/{created.Id}", created);
            });
                

            app.MapPut("api/products/{id:int}", (int id,Product product) => {
                var updated = ProductService.UpdateProduct(id, product);
                return updated ? Results.NoContent() : Results.NotFound();
            }).AddEndpointFilter<ValidationFilter<Product>>();

            app.MapDelete("api/products/{id:int}", (int id) => {
                var deleted = ProductService.DeleteProduct(id);
                return deleted ? Results.NoContent() : Results.NotFound();
            });


            var usersGroup = app.MapGroup("/api/users")
                .AddEndpointFilter<LoggingFilter>()
                .AddEndpointFilter<ValidationFilter<User>>();

            usersGroup.MapGet("/", () =>
            {
                return Results.Ok(UserService.GetAll());
            });

            usersGroup.MapGet("/{id:int}", (int id) =>
            {
                var user = UserService.GetById(id);
                return user is null
                    ? Results.NotFound()
                    : Results.Ok(user);
            });

            usersGroup.MapPost("/", (User user) =>
            {
                var created = UserService.Add(user);
                return Results.Created($"/api/users/{created.Id}", created);
            });





            app.Run();
        }
    }
}