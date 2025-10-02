using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPTypesAndKeywordsDemo
{
    public class ParentClass
    {
        public void Display()
        {
            Console.WriteLine("Base class display");
        }
    }

    public class ChildClass : ParentClass
    {
        public new void Display()
        {
            Console.WriteLine("Derived class display");
        }
    }
}
