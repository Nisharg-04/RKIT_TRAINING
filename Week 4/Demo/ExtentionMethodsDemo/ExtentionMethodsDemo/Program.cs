using System;
using System.Collections.Generic;
using System.Linq;
using ExtentionMethodsDemo;
class Program
{
    static void Main()
    {
    

        Console.WriteLine($"10.IsEven() -> {10.IsEven()}"); // True

        List<string?> names = new() { "Alice", null, "Bob" };
        Console.WriteLine($"names.ToCsv() -> {names.ToCsv()}"); // Alice,Bob
        List<int> ints = new() { 1, 2 };
        Console.WriteLine($"ints.ToCsv() -> {ints.ToCsv()}"); // 1,2,3,4
        var person = new Person
        {
            FirstName = "Tony",
            LastName = "Stark",
            DateOfBirth = new DateTime(1970, 5, 29)
        };
        Console.WriteLine($"Full Name: {person.GetFullName()}");
        var heroes = new List<Person>
        {
            person,
            new Person { FirstName = "Steve", LastName = "Rogers", DateOfBirth = new DateTime(1918, 7, 4)}
        };

    }
}