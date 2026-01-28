using System;
using System.Collections.Generic;
using System.Text.Json;

class Program
{
    static void Main()
    {
        var animals = new List<Animal>
        {
            new Dog { Name = "Rex", BarkVolume = 10 },
            new Cat { Name = "Whiskers", LikesMilk = true }
        };

        // Serialize using source generator
        string json = JsonSerializer.Serialize(animals);
        Console.WriteLine("Serialized JSON:");
        Console.WriteLine(json);

        // Deserialize
        var deserialized = JsonSerializer.Deserialize<List<Animal>>(json);
        Console.WriteLine("\nDeserialized objects:");
        foreach (var a in deserialized)
        {
            Console.WriteLine($"{a.Name} - {a.GetType().Name}");
        }
    }
}
