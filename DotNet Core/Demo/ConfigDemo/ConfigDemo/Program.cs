
using ConfigDemo.Services;
using ConfigDemo.Settings;

namespace ConfigDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.Configure<JwtSettings>(
                builder.Configuration.GetSection("Jwt"));

            builder.Services.Configure<EmailSettings>(
                builder.Configuration.GetSection("Email"));
            builder.Services.Configure<FeatureSettings>(
                builder.Configuration.GetSection("Features"));


            builder.Services.AddScoped<EmailService>();
            builder.Services.AddScoped<PaymentService>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
                app.UseSwagger();
                app.UseSwaggerUI();
            //}

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}