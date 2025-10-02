using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericCollectionsDemo
{
   
        public class DictionaryPropertiesDemo<TKey, TValue>
        {
            private Dictionary<TKey, TValue> _dictionary;

            public DictionaryPropertiesDemo()
            {
                _dictionary = new Dictionary<TKey, TValue>();
            }

            public void ShowProperties()
            {
                Console.WriteLine("Properties of Dictionary<TKey, TValue>");

                // 1. Comparer
                Console.WriteLine($"Comparer: {_dictionary.Comparer} (Gets the IEqualityComparer<T> that is used to determine equality of keys)");

                // 2. Count
                Console.WriteLine($"Count: {_dictionary.Count} (Gets the number of key/value pairs contained in the Dictionary)");

                // 3. Keys
                Console.WriteLine("Keys:");
                foreach (var key in _dictionary.Keys)
                {
                    Console.WriteLine(key);
                }

                // 4. Values
                Console.WriteLine("Values:");
                foreach (var value in _dictionary.Values)
                {
                    Console.WriteLine(value);
                }

            }
        }
    

}
