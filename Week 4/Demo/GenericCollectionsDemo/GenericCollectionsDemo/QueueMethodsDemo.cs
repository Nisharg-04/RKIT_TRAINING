using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericCollectionsDemo
{
    public class QueueMethodsDemo<T>
    {
        private Queue<T> _queue;

        public QueueMethodsDemo()
        {
            _queue = new Queue<T>();
        }

        public void ShowMethods()
        {
            Console.WriteLine("Methods of Queue<T>");

            // Enqueue — Add element
            _queue.Enqueue((T)(object)10!);
            _queue.Enqueue((T)(object)20!);
            _queue.Enqueue((T)(object)30!);
            Console.WriteLine($"After Enqueue: {string.Join(", ", _queue)}");

            // Peek — Look at first element without removing
            Console.WriteLine($"Peek: {_queue.Peek()}");

            // Dequeue — Remove first element
            var removed = _queue.Dequeue();
            Console.WriteLine($"Dequeued: {removed}");
            Console.WriteLine($"After Dequeue: {string.Join(", ", _queue)}");

            // Contains — Check if element exists
            Console.WriteLine($"Contains 20: {_queue.Contains((T)(object)20)}");

            // CopyTo — Copy elements to array
            T[] array = new T[_queue.Count];
            _queue.CopyTo(array, 0);
            Console.WriteLine("Copied Array: " + string.Join(", ", array));

            // Clear — Remove all elements
            //_queue.Clear();
            //Console.WriteLine("After Clear: Count = " + _queue.Count);
        }
    }
}
