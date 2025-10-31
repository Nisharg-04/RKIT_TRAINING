using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using EFCore.Models;

namespace EFCore
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Scaffold first
            // Scaffold-DbContext "Server=127.0.0.1;Database=OrmDemo;Uid=root;Pwd=yourpass;" Pomelo.EntityFrameworkCore.MySql -OutputDir Models

            const int iterations = 100;
            double totalTime = 0;

            var startDate = new DateTime(2025, 1, 10);
            var endDate = new DateTime(2026, 1, 1);
            string category = "Electronics";

            for (int i = 0; i < iterations; i++)
            {
                var stopwatch = Stopwatch.StartNew();

                using (var context = new OrmDemoContext())
                {
                    var result = context.Customers
                        .Join(context.Orders, c => c.Id, o => o.CustomerId, (c, o) => new { c, o })
                        .Join(context.Orderitems, co => co.o.Id, oi => oi.OrderId, (co, oi) => new { co.c, co.o, oi })
                        .Join(context.Products, coi => coi.oi.ProductId, p => p.Id, (coi, p) => new { coi.c, coi.o, coi.oi, p })
                        .Where(x =>
                            x.p.Category == category &&
                            x.o.OrderDate >= startDate &&
                            x.o.OrderDate < endDate)
                        .GroupBy(x => new { x.c.Id, x.c.Name })
                        .Select(g => new CustomerSpendingReport
                        {
                            CustomerName = g.Key.Name,
                            TotalOrders = g.Select(x => x.o.Id).Distinct().Count(),
                            TotalSpent = g.Sum(x => x.oi.UnitPrice * x.oi.Quantity)
                        })
                        .OrderByDescending(r => r.TotalSpent)
                        .Take(5)
                        .ToList();
                }

                stopwatch.Stop();
                totalTime += stopwatch.Elapsed.TotalMilliseconds;
            }

            double avgTime = totalTime / iterations;

            Console.WriteLine($"Executed {iterations} times.");
            Console.WriteLine($"Average EF Core query time: {avgTime:F2} ms");

            Console.WriteLine("\nTop 5 Customers:");

            using (var context = new OrmDemoContext())
            {
                var result = context.Customers
                    .Join(context.Orders, c => c.Id, o => o.CustomerId, (c, o) => new { c, o })
                    .Join(context.Orderitems, co => co.o.Id, oi => oi.OrderId, (co, oi) => new { co.c, co.o, oi })
                    .Join(context.Products, coi => coi.oi.ProductId, p => p.Id, (coi, p) => new { coi.c, coi.o, coi.oi, p })
                    .Where(x =>
                        x.p.Category == category &&
                        x.o.OrderDate >= startDate &&
                        x.o.OrderDate < endDate)
                    .GroupBy(x => new { x.c.Id, x.c.Name })
                    .Select(g => new CustomerSpendingReport
                    {
                        CustomerName = g.Key.Name,
                        TotalOrders = g.Select(x => x.o.Id).Distinct().Count(),
                        TotalSpent = g.Sum(x => x.oi.UnitPrice * x.oi.Quantity)
                    })
                    .OrderByDescending(r => r.TotalSpent)
                    .Take(5)
                    .ToList();

                foreach (var report in result)
                {
                    Console.WriteLine(report);
                }
            }
        }
    }
}
