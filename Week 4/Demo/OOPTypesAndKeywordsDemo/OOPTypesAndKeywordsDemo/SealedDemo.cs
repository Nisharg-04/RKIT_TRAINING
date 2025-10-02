using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPTypesAndKeywordsDemo
{
    public sealed class FinalClass
    {
        public void Display()
        {
            Console.WriteLine("This is a sealed class.");
        }
    }

    // class Derived : FinalClass {} //  Error: cannot inherit sealed class



    public class BaseClass
    {
        public virtual void Show()
        {
            Console.WriteLine("Base class method");
        }
    }

    public class DerivedClass : BaseClass
    {
        public sealed override void Show()
        {
            Console.WriteLine("Derived class sealed method");
        }
    }

    public class SubDerivedClass : DerivedClass
    {
        // public override void Show() {} //  Error: cannot override sealed method
    }
}
