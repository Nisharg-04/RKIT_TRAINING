
using Microsoft.EntityFrameworkCore;
using Reading_Room.Data;
using Reading_Room.Data.ReadingRoom.Data.Data;
using Reading_Room.DTO;
using Reading_Room.Models;
using Reading_Room.Services;

namespace Reading_Room
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = 
                       "Data Source=D:\\RKIT\\Github\\Week 5\\Assignment\\Reading Room\\Reading Room\\readingroom.db";


            builder.Services.AddDbContext<AppDbContext>(opts =>
                                                 opts.UseSqlite(connectionString));
            builder.Services.AddScoped<IRoomService, RoomService>();
            builder.Services.AddScoped<IReservationService, ReservationService>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Database.Migrate();
                 await DbInitializer.SeedAsync(db);

            }

            // Rooms endpoints
            app.MapGet("/rooms", async (IRoomService svc) => Results.Ok(await svc.GetAllAsync()));

            app.MapGet("/rooms/{id:int}", async (int id, IRoomService svc) =>
                await svc.GetByIdAsync(id) is RoomDto dto ? Results.Ok(dto) : Results.NotFound());

            app.MapPost("/rooms", async (RoomDto room, IRoomService svc) =>
            {
                // minimal validation
                if (string.IsNullOrWhiteSpace(room.Name)) return Results.ValidationProblem(new Dictionary<string, string[]> { { "Name", new[] { "Name is required" } } });
                var created = await svc.CreateAsync(room);
                return Results.Created($"/rooms/{created.Id}", created);
            });

            app.MapPut("/rooms/{id:int}", async (int id, RoomDto room, IRoomService svc) =>
            {
                if (!await svc.UpdateAsync(id, room)) return Results.NotFound();
                return Results.NoContent();
            });

            app.MapDelete("/rooms/{id:int}", async (int id, IRoomService svc) =>
                await svc.DeleteAsync(id) ? Results.NoContent() : Results.NotFound());

            // Reservations endpoints
            app.MapGet("/reservations", async (int? roomId, DateTime? from, DateTime? to, IReservationService svc) =>
            {
                var list = await svc.GetAsync(roomId, from, to);
                return Results.Ok(list.Select(r => new {
                    r.Id,
                    r.RoomId,
                    RoomName = r.Room?.Name,
                    r.PatronName,
                    r.Start,
                    r.End,
                    Status = r.Status.ToString()
                }));
            });

            app.MapGet("/reservations/{id:int}", async (int id, IReservationService svc) =>
            {
                ReservationDto r =  await svc.GetByIdAsync(id);
                return Results.Ok(r);


            });

            app.MapPost("/reservations", async (CreateReservationDto dto, IReservationService svc) =>
            {
                if (dto.End <= dto.Start)
                    return Results.ValidationProblem(new Dictionary<string, string[]> { { "End", new[] { "End must be after Start" } } });

                var res = new Reservation
                {
                    RoomId = dto.RoomId,
                    PatronName = dto.PatronName,
                    Start = dto.Start,
                    End = dto.End,
                    Status = Enum.TryParse<ReservationStatus>(dto.Status, true, out var st) ? st : ReservationStatus.Pending
                };

                var (success, error, created) = await svc.CreateAsync(res);
                if (!success) return Results.BadRequest(new { error });

                return Results.Created($"/reservations/{created!.Id}", created);
            });

            app.MapDelete("/reservations/{id:int}", async (int id, IReservationService svc) =>
                await svc.DeleteAsync(id) ? Results.NoContent() : Results.NotFound());


            // Analytics
            app.MapGet("/analytics/topbusiest", async (DateTime from, DateTime to, int top , IReservationService svc) =>
            {
                var data = await svc.TopNBusiestRoomsAsync(from, to, top);
                return Results.Ok(data.Select(d => new { d.RoomId, d.RoomName, BusyHours = d.BusyTime.TotalHours }));
            });

            app.MapGet("/analytics/conflicts", async (IReservationService svc) =>
            {
                var list = await svc.FindConflictingReservationsAsync();
                return Results.Ok(list.Select(c => new { c.RoomId, c.RoomName, c.Patron1, c.Patron2 }));
            });

            app.MapGet("/analytics/utilization", async (DateTime from, DateTime to, IReservationService svc) =>
            {
                var dict = await svc.UtilizationPercentPerRoomAsync(from, to);
                return Results.Ok(dict);
            });


            app.Run();
        }
    }
}
