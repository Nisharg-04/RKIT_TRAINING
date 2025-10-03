using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionDemo
{
  
        public class Person
        {
            public string Name { get; set; }
            private int _age;

            public Person() { }

            public Person(string name, int age)
            {
                Name = name;
                _age = age;
            }

            public void Greet()
            {
                Console.WriteLine($"Hi, I'm {Name}, {_age} years old.");
            }

            private void SecretMethod()
            {
                Console.WriteLine("This is a private secret!");
            }
        }

  }


