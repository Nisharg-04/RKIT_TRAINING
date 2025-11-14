using System;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.MySql;

namespace OrmLiteDemo
{
    public class CustomerSpendingReport
    {
        public string CustomerName { get; set; }
        public int TotalOrders { get; set; }
        public decimal TotalSpent { get; set; }

        public override string ToString()
        {
            return $"{CustomerName,-20} | Orders: {TotalOrders,3} | Spent: ₹{TotalSpent}";
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "server=localhost;user=root;password=nisharg2004;database=ormdemo;";

         
            var dbFactory = new OrmLiteConnectionFactory(connectionString, MySqlDialect.Provider);

            string sqlQuery = @"
                SELECT
                    c.Name AS CustomerName,
                    COUNT(DISTINCT o.Id) AS TotalOrders,
                    SUM(oi.Quantity * oi.UnitPrice) AS TotalSpent
                FROM
                    Customers c
                JOIN
                    Orders o ON c.Id = o.CustomerId
                JOIN
                    OrderItems oi ON o.Id = oi.OrderId
                JOIN
                    Products p ON oi.ProductId = p.Id
                WHERE
                    p.Category = @Category
                    AND o.OrderDate >= @StartDate
                    AND o.OrderDate < @EndDate
                GROUP BY
                    c.Id, c.Name
                ORDER BY
                    TotalSpent DESC
                LIMIT 5;
            ";

            var parameters = new
            {
                Category = "Electronics",
                StartDate = new DateTime(2025, 1, 10),
                EndDate = new DateTime(2026, 1, 1)
            };

            const int iterations = 100;
            double totalTime = 0;

     
            for (int i = 0; i < iterations; i++)
            {
                var stopwatch = Stopwatch.StartNew();

                using (var db = dbFactory.Open())
                {
                    var result = db.Select<CustomerSpendingReport>(sqlQuery, parameters);
                }

                stopwatch.Stop();
                totalTime += stopwatch.Elapsed.TotalMilliseconds;
            }

            double avgTime = totalTime / iterations;
            Console.WriteLine($"Executed {iterations} times.");
            Console.WriteLine($"Average query time: {avgTime:F2} ms");

            
            using (var db = dbFactory.Open())
            {
                var result = db.Select<CustomerSpendingReport>(sqlQuery, parameters);
                Console.WriteLine("\nTop 5 Customers:");
                foreach (var item in result)
                    Console.WriteLine(item);
            }
        }
    }
}
