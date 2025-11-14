using Microsoft.EntityFrameworkCore;
using Reading_Room.Data;
using Reading_Room.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ReadingRoomAnalytics
{
    internal class ListDemo
    {
        public static async Task listDemo(AppDbContext db)
        {

            DateTime from; 
            DateTime.TryParse("2025-10-31 00:00:00", out from);
            from =from.AddHours(8);
            DateTime to;
            DateTime.TryParse("2025-10-31 00:00:00", out to);
            to=to.AddHours(16);
            Console.WriteLine($"Analyzing reservations from {from} to {to}\n");
            var list = await db.Reservations
                .Include(r => r.Room)
                .Where(r => r.End > from && r.Start < to && r.Status != ReservationStatus.Cancelled)
                .ToListAsync();

            // N busiest rooms
            var busiestRooms = list
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
                 .Take(3)
                 .Select(x => (x.RoomId, x.RoomName, TimeSpan.FromMinutes(x.TotalMinutes)))
                 .ToList();

            Console.WriteLine("Busiest Rooms:");
            foreach (var room in busiestRooms)
            {
                Console.WriteLine(room);
            }


            var query =
       from a in db.Reservations
       from b in db.Reservations
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
            Console.WriteLine("\nOverlapping Reservations:");
            foreach (var r in results)
            {
                Console.WriteLine($"RoomId: {r.RoomId}, RoomName: {r.RoomName}, Patron1: {r.Patron1}, Patron2: {r.Patron2}");
            }


            //utilization percent per room

            var totalWindowMinutes = (to - from).TotalMinutes;
            var usage = list
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
            var rooms = await db.Rooms.ToListAsync();
            var result = new Dictionary<int, double>();
            foreach (var room in rooms)
            {
                usage.TryGetValue(room.Id, out var usedMinutes);
                result[room.Id] = (usedMinutes / totalWindowMinutes) * 100.0;
            }

            Console.WriteLine("\nUtilization Percent Per Room:");
            foreach (var kvp in result)
            {
                Console.WriteLine($"RoomId: {kvp.Key}, Utilization: {kvp.Value:F2}%");
            }





            }
    }
}
