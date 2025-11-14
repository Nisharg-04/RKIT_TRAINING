using Microsoft.EntityFrameworkCore;
using Reading_Room.Data;
using Reading_Room.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingRoomAnalytics
{
     static class DataTableDemo
    {
        public static DataTable ToDataTable<T>(this List<T> items)
        {
            var table = new DataTable(typeof(T).Name);
            var props = typeof(T).GetProperties();

            foreach (var prop in props)
            {
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            foreach (var item in items)
            {
                var values = new object?[props.Length];
                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }

            return table;
        }
        public static async Task DataTableQuery(AppDbContext db)
        {
            var reservations = await db.Reservations
     .Include(r => r.Room)
     .Select(r => new
     {
         r.Id,
         r.RoomId,
         RoomName = r.Room.Name,
         r.Start,
         r.End,
         r.Status,
         r.PatronName
     })
     .ToListAsync();

            var rooms = await db.Rooms.ToListAsync();
            var resTable = reservations.ToDataTable();
            var roomTable = rooms.ToDataTable();
            DateTime from;
            DateTime.TryParse("2025-10-31 00:00:00", out from);
            from = from.AddHours(8);
            DateTime to;
            DateTime.TryParse("2025-10-31 00:00:00", out to);
            to = to.AddHours(16);


            //busy rooms 
            var list = resTable.AsEnumerable()
                .Where(r => r.Field<DateTime>("End") > from &&
                            r.Field<DateTime>("Start") < to &&
                           r.Field<int>("Status") != (int)ReservationStatus.Cancelled)
                .GroupBy(r => r.Field<int>("RoomId"))
                .Select(g =>
                {
                    var total = g.Sum(r =>
                    {
                        var s = r.Field<DateTime>("Start") < from ? from : r.Field<DateTime>("Start");
                        var e = r.Field<DateTime>("End") > to ? to : r.Field<DateTime>("End");
                        return (e - s).TotalMinutes;
                    });

                    return new
                    {
                        RoomId = g.Key,
                        RoomName = g.First().Field<string>("RoomName"),
                        TotalMinutes = total
                    };
                })
                .OrderByDescending(x => x.TotalMinutes)
                .Take(3)
                .Select(x => (x.RoomId, x.RoomName, TimeSpan.FromMinutes(x.TotalMinutes)))
                .ToList();

            Console.WriteLine("Busiest Rooms (DataTable):");
            foreach (var room in list)
            {
                Console.WriteLine(room);
            }


            // Confilcts 
            var listA = resTable.AsEnumerable();
            var listB = resTable.AsEnumerable();

            var conflicts =
                (from a in listA
                 from b in listB
                 where a.Field<int>("Id") < b.Field<int>("Id")
                       && a.Field<int>("RoomId") == b.Field<int>("RoomId")
                       && a.Field<int>("Status") != (int)ReservationStatus.Cancelled
                       && b.Field<int>("Status") != (int)ReservationStatus.Cancelled
                       && a.Field<DateTime>("Start") < b.Field<DateTime>("End")
                       && b.Field<DateTime>("Start") < a.Field<DateTime>("End")
                 select (
                     RoomId: a.Field<int>("RoomId"),
                     RoomName: a.Field<string>("RoomName"),
                     Patron1: a.Field<string>("PatronName"),
                     Patron2: b.Field<string>("PatronName")
                 )).ToList();

            Console.WriteLine("\nConflicting Reservations (DataTable):");
            foreach (var room in conflicts)
            {
                Console.WriteLine(room);
            }


            // utilization percentage
            var totalWindowMinutes = (to - from).TotalMinutes;
            var usage = resTable.AsEnumerable()
                .Where(r => r.Field<DateTime>("End") > from &&
                            r.Field<DateTime>("Start") < to &&
                            r.Field<int>("Status") != (int)ReservationStatus.Cancelled)
                .GroupBy(r => r.Field<int>("RoomId"))
                .ToDictionary(
                    g => g.Key,
                    g => g.Sum(r =>
                    {
                        var s = r.Field<DateTime>("Start") < from ? from : r.Field<DateTime>("Start");
                        var e = r.Field<DateTime>("End") > to ? to : r.Field<DateTime>("End");
                        return (e - s).TotalMinutes;
                    })
                );
            var result = new Dictionary<int, double>();
            foreach (DataRow room in roomTable.Rows)
            {
                int roomId = room.Field<int>("Id");
                usage.TryGetValue(roomId, out var usedMinutes);
                result[roomId] = (usedMinutes / totalWindowMinutes) * 100.0;
            }

            Console.WriteLine("\nUtilization Percentage per Room (DataTable):");
            foreach (var kvp in result)
            {
                Console.WriteLine($"RoomId: {kvp.Key}, Utilization: {kvp.Value:F2}%");
              
            }
            }   
    }
}
