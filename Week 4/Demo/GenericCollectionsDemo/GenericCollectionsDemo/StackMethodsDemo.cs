using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericCollectionsDemo
{
    public class StackMethodsDemo<T>
    {
        private Stack<T> _stack;

        public StackMethodsDemo()
        {
            _stack = new Stack<T>();
        }

        public void ShowMethods()
        {
            Console.WriteLine("---- Methods of Stack<T> ----");

            // Push — Add element to top
            _stack.Push((T)(object)10!);
            _stack.Push((T)(object)20!);
            _stack.Push((T)(object)30!);
            Console.WriteLine($"After Push: {string.Join(", ", _stack)}");

            // Peek — Look at top element without removing
            Console.WriteLine($"Peek: {_stack.Peek()}");

            // Pop — Remove and return top element
            var popped = _stack.Pop();
            Console.WriteLine($"Popped: {popped}");
            Console.WriteLine($"After Pop: {string.Join(", ", _stack)}");

            // Contains — Check if element exists
            Console.WriteLine($"Contains 20: {_stack.Contains((T)(object)20)}");

            // CopyTo — Copy elements to array
            T[] array = new T[_stack.Count];
            _stack.CopyTo(array, 0);
            Console.WriteLine("Copied Array: " + string.Join(", ", array));

            // Clear — Remove all elements
            //_stack.Clear();
            //Console.WriteLine("After Clear: Count = " + _stack.Count);
            //Console.WriteLine("After Clear: Count = " + _stack.Count);
        }
    }
}
