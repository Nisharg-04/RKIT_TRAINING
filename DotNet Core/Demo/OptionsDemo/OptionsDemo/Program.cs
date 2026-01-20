
using OptionsDemo.Options;
using OptionsDemo.Services;

namespace OptionsDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
            builder.Services.Configure<EmailSettings>(
            "Gmail",
            builder.Configuration.GetSection("EmailSettings:Gmail"));

            builder.Services.Configure<EmailSettings>(
            "Outlook",
            builder.Configuration.GetSection("EmailSettings:Outlook"));
            builder.Services.AddSingleton<ConfigWatcher>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            app.Services.GetRequiredService<ConfigWatcher>();

            app.Run();
        }
    }
}