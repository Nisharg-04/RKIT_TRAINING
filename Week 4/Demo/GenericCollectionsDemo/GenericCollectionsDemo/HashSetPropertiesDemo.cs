using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericCollectionsDemo
{  

        public class HashSetPropertiesDemo<T>
        {
            private HashSet<T> _hashSet;

            public HashSetPropertiesDemo()
            {
                _hashSet = new HashSet<T>();
            }

            public void ShowProperties()
            {
                Console.WriteLine("Properties of HashSet<T>");

                // Comparer
                Console.WriteLine($"Comparer: {_hashSet.Comparer} (Gets the equality comparer for the HashSet)");

                // Count
                Console.WriteLine($"Count: {_hashSet.Count} (Gets the number of elements in the HashSet)");

                
            }
        }
    }


