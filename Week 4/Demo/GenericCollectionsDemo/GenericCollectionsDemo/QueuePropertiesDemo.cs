using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericCollectionsDemo
{
    public class QueuePropertiesDemo<T>
    {
        private Queue<T> _queue;

        public QueuePropertiesDemo()
        {
            _queue = new Queue<T>();
        }

        public void ShowProperties()
        {
            Console.WriteLine("Properties of Queue<T>");

            // Count
            Console.WriteLine($"Count: {_queue.Count} (Gets the number of elements in the Queue)");
        }
    }
}
