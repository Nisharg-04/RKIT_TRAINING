using System.Collections.Generic;

namespace SerializationDemo
{
    public class SuperHero
    {
        public string Name { get; set; }
        public string SecretIdentity { get; set; }
        public int Age { get; set; }
        public Address HeroAddress { get; set; } // nested object
        public List<string> Superpowers { get; set; } // collection
    }
}