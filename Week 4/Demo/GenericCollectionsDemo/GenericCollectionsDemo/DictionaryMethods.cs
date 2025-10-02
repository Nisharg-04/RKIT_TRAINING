using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericCollectionsDemo
{
 
    
        public class DictionaryMethodsDemo<TKey, TValue>
        {
            private Dictionary<TKey, TValue> _dictionary;

            public DictionaryMethodsDemo()
            {
                _dictionary = new Dictionary<TKey, TValue>();
            }

            public void ShowMethods()
            {
                Console.WriteLine("Methods of Dictionary<TKey, TValue>");

                // Adding items
                _dictionary.Add((TKey)(object)1, (TValue)(object)"One"); // Add()
                _dictionary.Add((TKey)(object)2, (TValue)(object)"Two");
               

                // Accessing
                Console.WriteLine($"Indexer access: {_dictionary[(TKey)(object)1]}");

                // ContainsKey / ContainsValue
                Console.WriteLine($"ContainsKey(2): {_dictionary.ContainsKey((TKey)(object)2)}");
                Console.WriteLine($"ContainsValue(\"Three\"): {_dictionary.ContainsValue((TValue)(object)"Three")}");

                // TryGetValue
                if (_dictionary.TryGetValue((TKey)(object)2, out TValue value))
                {
                    Console.WriteLine($"TryGetValue(2): {value}");
                }

                // Remove
                _dictionary.Remove((TKey)(object)1);
                Console.WriteLine("Removed key 1");

                // Clear
                //_dictionary.Clear();
                //Console.WriteLine("Cleared dictionary");

                // Iterating
                Console.WriteLine("Iterating dictionary:");
                foreach (var kvp in _dictionary)
                {
                    Console.WriteLine($"Key: {kvp.Key}, Value: {kvp.Value}");
                }
            }
        }
    
}
