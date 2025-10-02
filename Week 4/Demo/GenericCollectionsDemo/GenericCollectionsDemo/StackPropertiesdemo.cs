using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericCollectionsDemo
{
    public class StackPropertiesDemo<T>
    {
        private Stack<T> _stack;

        public StackPropertiesDemo()
        {
            _stack = new Stack<T>();
        }

        public void ShowProperties()
        {
            Console.WriteLine("Properties of Stack<T>");

            // Count
            Console.WriteLine($"Count: {_stack.Count} (Gets the number of elements in the Stack)");

        }
    }

}
