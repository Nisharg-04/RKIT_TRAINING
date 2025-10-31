using Microsoft.Extensions.Hosting;
using Reading_Room.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace ReadingRoomAnalytics
{
    internal class Program

    {
        static async Task Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
  
    .ConfigureServices((context, services) =>
    {
       
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite("Data Source=D:\\RKIT\\Github\\Week 5\\Assignment\\Reading Room\\Reading Room\\readingroom.db"));
    })
    .Build();

            using var scope = host.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            //ListDemo.listDemo(db);
            await DataTableDemo.DataTableQuery(db);





        }
    }
}
