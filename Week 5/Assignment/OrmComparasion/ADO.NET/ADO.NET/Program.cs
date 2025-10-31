using System;
using System.Diagnostics;
using MySql.Data.MySqlClient;

namespace ADO_NET
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "server=localhost;user=root;password=nisharg2004;database=ormdemo;";

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

            const int iterations = 100;
            double totalTime = 0;

            for (int i = 0; i < iterations; i++)
            {
                var stopwatch = Stopwatch.StartNew();

                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    using (var cmd = new MySqlCommand(sqlQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Category", "Electronics");
                        cmd.Parameters.AddWithValue("@StartDate", new DateTime(2025, 10, 1));
                        cmd.Parameters.AddWithValue("@EndDate", new DateTime(2026, 1, 1));

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                _ = reader["CustomerName"];
                            }
                        }
                    }
                }

                stopwatch.Stop();
                totalTime += stopwatch.Elapsed.TotalMilliseconds;
            }

            double avgTime = totalTime / iterations;

            Console.WriteLine($"Executed {iterations} times.");
            Console.WriteLine($"Average query time: {avgTime:F2} ms");

   
            Console.WriteLine("\nTop 5 Customers:");

            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new MySqlCommand(sqlQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Category", "Electronics");
                    cmd.Parameters.AddWithValue("@StartDate", new DateTime(2025, 10, 1));
                    cmd.Parameters.AddWithValue("@EndDate", new DateTime(2026, 1, 1));

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string customerName = reader.GetString("CustomerName");
                            int totalOrders = reader.GetInt32("TotalOrders");
                            decimal totalSpent = reader.GetDecimal("TotalSpent");

                            Console.WriteLine($"Name: {customerName}, Orders: {totalOrders}, Spent: {totalSpent:C}");
                        }
                    }
                }
            }
        }
    }
}
