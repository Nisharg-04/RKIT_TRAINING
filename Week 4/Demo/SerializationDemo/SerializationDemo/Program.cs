using System;
using System.Collections.Generic;
using System.IO;
// For JSON Serialization
using System.Text.Json;
// For XML Serialization
using System.Xml.Serialization;

namespace SerializationDemo
{
    class Program
    {
        static void Main(string[] args)
        {
          
            var hero = new SuperHero
            {
                Name = "Iron Man",
                SecretIdentity = "Tony Stark",
                Age = 48,
                HeroAddress = new Address
                {
                    Street = "10880 Malibu Point",
                    City = "Malibu",
                    ZipCode = "90265"
                },
                Superpowers = new List<string>
                {
                    "Genius-level intellect",
                    "Powered armor suit",
                    "Vast wealth"
                }
            };

            JsonSerializationDemo(hero);
            XmlSerializationDemo(hero);

       
        }

        public static void JsonSerializationDemo(SuperHero hero)
        {
            Console.WriteLine("JSON DEMO");


            var options = new JsonSerializerOptions { WriteIndented = true }; //pretty print
            string jsonString = JsonSerializer.Serialize(hero, options);

            Console.WriteLine("\nObject Serialized to JSON:");
            Console.WriteLine(jsonString);

             File.WriteAllText("hero.json", jsonString);

 
            SuperHero deserializedHero = JsonSerializer.Deserialize<SuperHero>(jsonString);

            Console.WriteLine("\nJSON Deserialized back to Object:");
            Console.WriteLine($"Hero Name: {deserializedHero.Name}");
            Console.WriteLine($"Secret Identity: {deserializedHero.SecretIdentity}");
            Console.WriteLine($"City: {deserializedHero.HeroAddress.City}");
            Console.WriteLine($"First Superpower: {deserializedHero.Superpowers[0]}\n");
        }

        public static void XmlSerializationDemo(SuperHero hero)
        {
            Console.WriteLine("XML DEMO");

       
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(SuperHero));

            using (var stringWriter = new StringWriter())
            {
                xmlSerializer.Serialize(stringWriter, hero);
                string xmlString = stringWriter.ToString();

                Console.WriteLine("\nObject Serialized to XML:");
                Console.WriteLine(xmlString);

                 File.WriteAllText("hero.xml", xmlString);

                using (var stringReader = new StringReader(xmlString))
                {
                    SuperHero deserializedHero = (SuperHero)xmlSerializer.Deserialize(stringReader);

                    Console.WriteLine("\nXML Deserialized back to Object:");
                    Console.WriteLine($"Hero Name: {deserializedHero.Name}");
                    Console.WriteLine($"Secret Identity: {deserializedHero.SecretIdentity}");
                    Console.WriteLine($"City: {deserializedHero.HeroAddress.City}");
                    Console.WriteLine($"First Superpower: {deserializedHero.Superpowers[0]}\n");
                }
            }
        }
    }
}