namespace Reading_Room.Data
{
    using Microsoft.EntityFrameworkCore;
    using Reading_Room.Models;

    namespace ReadingRoom.Data.Data
    {
        public static class DbInitializer
        {
            public static async Task SeedAsync(AppDbContext context)
            {
                // apply pending migrations if any
                await context.Database.MigrateAsync();

                // Seed Rooms if none exist
                if (!context.Rooms.Any())
                {
                    var rooms = new List<Room>
                {
                    new Room { Name = "Quiet Study Room", Capacity = 4 },
                    new Room { Name = "Main Reading Hall", Capacity = 20 },
                    new Room { Name = "Group Discussion Room", Capacity = 8 },
                    new Room { Name = "Reference Section", Capacity = 10 },
                };

                    await context.Rooms.AddRangeAsync(rooms);
                    await context.SaveChangesAsync();
                }

                // Seed Reservations if none exist
                if (!context.Reservations.Any())
                {
                    var rooms = await context.Rooms.ToListAsync();

                    var reservations = new List<Reservation>
                {
                    new Reservation
                    {
                        RoomId = rooms[0].Id,
                        PatronName = "Alice",
                        Start = DateTime.Today.AddHours(9),
                        End = DateTime.Today.AddHours(11),
                        Status = ReservationStatus.Confirmed
                    },
                            new Reservation
                    {
                        RoomId = rooms[0].Id,
                        PatronName = "Conflict Alice",
                        Start = DateTime.Today.AddHours(10),
                        End = DateTime.Today.AddHours(14),
                        Status = ReservationStatus.Confirmed
                    },
                    new Reservation
                    {
                        RoomId = rooms[0].Id,
                        PatronName = "Bob",
                        Start = DateTime.Today.AddHours(13),
                        End = DateTime.Today.AddHours(15),
                        Status = ReservationStatus.Confirmed
                    },
                    new Reservation
                    {
                        RoomId = rooms[1].Id,
                        PatronName = "Charlie",
                        Start = DateTime.Today.AddHours(10),
                        End = DateTime.Today.AddHours(14),
                        Status = ReservationStatus.Confirmed
                    },
                    new Reservation
                    {
                        RoomId = rooms[2].Id,
                        PatronName = "David",
                        Start = DateTime.Today.AddHours(8),
                        End = DateTime.Today.AddHours(10),
                        Status = ReservationStatus.Confirmed
                    },
                    new Reservation
                    {
                        RoomId = rooms[2].Id,
                        PatronName = "Emma",
                        Start = DateTime.Today.AddHours(11),
                        End = DateTime.Today.AddHours(13),
                        Status = ReservationStatus.Cancelled
                    },
                };

                    await context.Reservations.AddRangeAsync(reservations);
                    await context.SaveChangesAsync();
                }
            }
        }
    }

}
