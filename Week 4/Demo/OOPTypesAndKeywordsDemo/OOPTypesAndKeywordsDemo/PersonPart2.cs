using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPTypesAndKeywordsDemo
{
    public partial class Person1
    {
        public int Age { get; set; }

        public void Display()
        {
            Console.WriteLine($"Name: {Name}, Age: {Age}");
        }
    }

}
