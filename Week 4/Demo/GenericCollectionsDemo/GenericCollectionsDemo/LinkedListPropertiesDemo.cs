using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericCollectionsDemo
{
    public class LinkedListPropertiesDemo<T>
    {
        private LinkedList<T> _linkedList;

        public LinkedListPropertiesDemo()
        {
            _linkedList = new LinkedList<T>();
        }

        public void ShowProperties()
        {
            Console.WriteLine("Properties of LinkedList<T>");

            // Count
            Console.WriteLine($"Count: {_linkedList.Count} (Gets the number of nodes in the LinkedList)");

            // First
            Console.WriteLine($"First: {_linkedList.First} (Gets the first node of the LinkedList)");

            // Last
            Console.WriteLine($"Last: {_linkedList.Last} (Gets the last node of the LinkedList)");

        }
    }
}
