using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPTypesAndKeywordsDemo
{
    public abstract class Animal
    {
        public abstract void MakeSound();

        public void Eat()
        {
            Console.WriteLine("Eating");
        }
    }

    public class Dog : Animal
    {
        public override void MakeSound()
        {
            Console.WriteLine("Bark");
        }
    }
}
