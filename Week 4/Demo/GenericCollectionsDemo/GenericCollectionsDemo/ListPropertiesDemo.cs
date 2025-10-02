using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericCollectionsDemo
{
    public class ListPropertiesDemo<T>
    {
        private List<T> _list;

        public ListPropertiesDemo()
        {
            _list = new List<T>();
        }

        public void ShowProperties()
        {
            Console.WriteLine("Properties of List<T>");

            // 1. Capacity
            Console.WriteLine($"Capacity: {_list.Capacity} (Gets or sets the total number of elements the internal data structure can hold)");

            // 2. Count
            Console.WriteLine($"Count: {_list.Count} (Gets the number of elements actually contained in the List<T>)");

        }
    }
}
