using Reading_Room.Models;
using Microsoft.EntityFrameworkCore;

namespace Reading_Room.Data
{
    /// <summary>
    /// A DbContext for the application, managing Room and Reservation entities.
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }

        public DbSet<Room> Rooms => Set<Room>();
        public DbSet<Reservation> Reservations => Set<Reservation>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // configure any indexes for faster conflict checks
            modelBuilder.Entity<Reservation>()
                .HasIndex(r => new { r.RoomId, r.Start, r.End });

            // seed data (small)
            modelBuilder.Entity<Room>().HasData(
                new Room { Id = 1, Name = "Room A", Capacity = 6 },
                new Room { Id = 2, Name = "Room B", Capacity = 10 },
                new Room { Id = 3, Name = "Room C", Capacity = 4 }
            );

           
        }
    }
}
