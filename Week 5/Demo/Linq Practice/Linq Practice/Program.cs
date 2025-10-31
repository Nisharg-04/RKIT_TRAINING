using System.Windows.Markup;

namespace Linq_Practice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Product> products = new List<Product>
                    {
                        new Product { Id = 1, Name = "Laptop", Category = "Electronics", Price = 1200.00m },
                        new Product { Id = 2, Name = "Smartphone", Category = "Electronics", Price = 800.00m },
                        new Product { Id = 3, Name = "Coffee Maker", Category = "Appliances", Price = 150.00m },
                        new Product { Id = 4, Name = "Headphones", Category = "Electronics", Price = 250.00m },
                        new Product { Id = 5, Name = "The C# Guide", Category = "Books", Price = 45.00m },
                        new Product { Id = 6, Name = "Blender", Category = "Appliances", Price = 80.00m }
                    };

            // 2. Customer List
            List<Customer> customers = new List<Customer>
                    {
                        new Customer { Id = 101, Name = "Alice Smith", City = "New York" },
                        new Customer { Id = 102, Name = "Bob Johnson", City = "London" },
                        new Customer { Id = 103, Name = "Charlie Brown", City = "New York" },
                        new Customer { Id = 104, Name = "David Lee", City = "Tokyo" }
                    };

            // 3. Order List
            List<Order> orders = new List<Order>
                    {
                        new Order { OrderId = 1, CustomerId = 101, OrderDate = new DateTime(2025, 1, 15) },
                        new Order { OrderId = 2, CustomerId = 102, OrderDate = new DateTime(2025, 1, 17) },
                        new Order { OrderId = 3, CustomerId = 101, OrderDate = new DateTime(2025, 2, 1) },
                        new Order { OrderId = 4, CustomerId = 104, OrderDate = new DateTime(2025, 2, 5) }
                    };

            // 4. OrderDetail List
            List<OrderDetail> orderDetails = new List<OrderDetail>
                    {
                        new OrderDetail { OrderId = 1, ProductId = 1, Quantity = 1 }, // Alice buys 1 Laptop
                        new OrderDetail { OrderId = 1, ProductId = 4, Quantity = 1 }, // Alice buys 1 Headphones
                        new OrderDetail { OrderId = 2, ProductId = 2, Quantity = 2 }, // Bob buys 2 Smartphones
                        new OrderDetail { OrderId = 3, ProductId = 5, Quantity = 5 }, // Alice buys 5 C# Guides
                        new OrderDetail { OrderId = 4, ProductId = 3, Quantity = 1 }  // David buys 1 Coffee Maker
                    };


            //var eleproducts = products.Where(p => p.Category == "Electronics");
            //foreach (var p in eleproducts)
            //{
            //    Console.WriteLine($"{p.Name}");
            //}
            //var selectProducts = products.Select(p => new
            //{
            //    productname = p.Name,
            //    productprice = p.Price
            //});
            //foreach (var p in selectProducts)
            //{
            //    Console.WriteLine($"{p.productname} : {p.productprice}");

            //}

            //var sortedproducts = products.OrderByDescending(p => p.Price);
            //foreach (var p in sortedproducts)
            //{
            //    Console.WriteLine($"{p.Name} : {p.Price}");
            //}


            //var groupedProducts = products.GroupBy(p => p.Category).Select(g =>
            //new
            //{
            //    Category = g.Key,
            //    CategoryCount = g.Count(),
            //    CategoryAverage = g.Average(p => p.Price)
            //});

            //foreach (var g in groupedProducts)
            //{
            //    Console.WriteLine($"{g.Category} : {g.CategoryCount} : {g.CategoryAverage}");
            //}

            //var simpleJoin = customers.Join(orders, c => c.Id, o => o.CustomerId, (c, o) => new { c.Name, o.OrderDate });
            //foreach (var item in simpleJoin)
            //{
            //    Console.WriteLine($"{item.Name} : {item.OrderDate}");
            //}

            //var simplejoinquery = from c in customers
            //                      join o in orders on c.Id equals o.CustomerId
            //                      select new { c.Name, o.OrderDate };

            //foreach (var item in simplejoinquery)
            //{                 Console.WriteLine($"{item.Name} : {item.OrderDate}"); }



        }                            
    }
}
