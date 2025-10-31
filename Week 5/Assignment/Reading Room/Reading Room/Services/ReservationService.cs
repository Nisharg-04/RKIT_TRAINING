using Reading_Room.Data;
using Reading_Room.Models;
using Microsoft.EntityFrameworkCore;
using Reading_Room.DTO;
namespace Reading_Room.Services
{
    public class ReservationService : IReservationService
    {
        private readonly AppDbContext _db;
        public ReservationService(AppDbContext db) => _db = db;

        public async Task<ReservationDto?> GetByIdAsync(int id) {
          return  await _db.Reservations.Where(r => r.Id == id).Select(r => new ReservationDto(
        r.Id,
        r.PatronName,
        r.Start,
        r.End,
        r.Status,
        r.Room.Name
    ))
    .FirstOrDefaultAsync();
        }
        public async Task<List<Reservation>> GetAsync(int? roomId, DateTime? from, DateTime? to)
        {
            var q = _db.Reservations.Include(r => r.Room).AsQueryable();
            if (roomId.HasValue) q = q.Where(r => r.RoomId == roomId.Value);
            if (from.HasValue) q = q.Where(r => r.End >= from.Value);
            if (to.HasValue) q = q.Where(r => r.Start <= to.Value);
            return await q.ToListAsync();
        }

        public async Task<(bool success, string? error, Reservation? reservation)> CreateAsync(Reservation res)
        {
            // validation
            if (res.End <= res.Start)
                return (false, "End must be after Start", null);

            // conflict check: for same room, overlapping confirmed/pending reservations
            var overlap = await _db.Reservations
                .Where(r => r.RoomId == res.RoomId && r.Status != ReservationStatus.Cancelled)
                .Where(r => r.Start < res.End && res.Start < r.End)
                .AnyAsync();

            if (overlap)
                return (false, "The room is already booked for that time range", null);

            _db.Reservations.Add(res);
            await _db.SaveChangesAsync();
            return (true, null, res);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var r = await _db.Reservations.FindAsync(id);
            if (r == null) return false;
            _db.Reservations.Remove(r);
            await _db.SaveChangesAsync();
            return true;
        }

        // Analytics
        // Top N busiest rooms
        public async Task<List<(int RoomId, string RoomName, TimeSpan BusyTime)>> TopNBusiestRoomsAsync(DateTime from, DateTime to, int topN)
        {
            
            var list = await _db.Reservations
                .Include(r => r.Room)
                .Where(r => r.End > from && r.Start < to && r.Status != ReservationStatus.Cancelled)
                .ToListAsync();

            var grouped = list
                .GroupBy(r => r.RoomId)
                .Select(g =>
                {
                    var total = g.Sum(r =>
                    {
                        var s = r.Start < from ? from : r.Start;
                        var e = r.End > to ? to : r.End;
                        return (e - s).TotalMinutes;
                    });
                    return new { RoomId = g.Key, RoomName = g.First().Room!.Name, TotalMinutes = total };
                })
                .OrderByDescending(x => x.TotalMinutes)
                .Take(topN)
                .Select(x => (x.RoomId, x.RoomName, TimeSpan.FromMinutes(x.TotalMinutes)))
                .ToList();

            return grouped;
        }


        public async Task<List<(int RoomId, string RoomName, string Patron1, string Patron2)>> FindConflictingReservationsAsync()
        {
            var query =
       from a in _db.Reservations
       from b in _db.Reservations
       where
           a.Id < b.Id &&                 // avoid duplicates
           a.RoomId == b.RoomId &&        // same room
           a.Status != ReservationStatus.Cancelled &&
           b.Status != ReservationStatus.Cancelled &&
           a.Start < b.End && b.Start < a.End  // overlap
       select new
       {
           a.RoomId,
           RoomName = a.Room.Name,
           Patron1 = a.PatronName,
           Patron2 = b.PatronName
       };

            var results = await query.ToListAsync();
            return results.Select(r => (r.RoomId, r.RoomName, r.Patron1, r.Patron2)).ToList();
        }

        public async Task<Dictionary<int, double>> UtilizationPercentPerRoomAsync(DateTime from, DateTime to)
        {
            var totalWindowMinutes = (to - from).TotalMinutes;
            if (totalWindowMinutes <= 0) return new();

            var resList = await _db.Reservations
                .Include(r => r.Room)
                .Where(r => r.End > from && r.Start < to && r.Status != ReservationStatus.Cancelled)
                .ToListAsync();

            var usage = resList
                .GroupBy(r => r.RoomId)
                .ToDictionary(
                    g => g.Key,
                    g => g.Sum(r =>
                    {
                        var s = r.Start < from ? from : r.Start;
                        var e = r.End > to ? to : r.End;
                        return (e - s).TotalMinutes;
                    })
                );

            // compute percentage per room
            var rooms = await _db.Rooms.ToListAsync();
            var result = new Dictionary<int, double>();
            foreach (var room in rooms)
            {
                usage.TryGetValue(room.Id, out var usedMinutes);
                result[room.Id] = (usedMinutes / totalWindowMinutes) * 100.0;
            }
            return result;
        }
    }
    }
