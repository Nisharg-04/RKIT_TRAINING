using System.Text.Json;

namespace SourceGeneratorusingJson
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var user = new User
            {
                Id = 1,
                Name = "Nisharg",
                Email = "nisharg@example.com",
                IsAdmin = true
            };

   
            string json = JsonSerializer.Serialize(
                user,
                AppJsonContext.Default.User
            );

            Console.WriteLine("Serialized JSON:");
            Console.WriteLine(json);
        }
    }
    }
