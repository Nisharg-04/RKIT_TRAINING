using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericCollectionsDemo
{
    
        public class HashSetMethodsDemo<T>
        {
            private HashSet<T> _hashSet;

            public HashSetMethodsDemo()
            {
                _hashSet = new HashSet<T>();
            }

            public void ShowMethods()
            {
                Console.WriteLine("Methods of HashSet<T>");

                // Adding
                _hashSet.Add((T)(object)1!);
                _hashSet.Add((T)(object)2!);
                _hashSet.Add((T)(object)3!);

                Console.WriteLine($"Added elements: {string.Join(", ", _hashSet)}");

                // Contains
                Console.WriteLine($"Contains(2): {_hashSet.Contains((T)(object)2)}");

                // Remove
                _hashSet.Remove((T)(object)2);
                Console.WriteLine($"After Remove(2): {string.Join(", ", _hashSet)}");

                // Clear
                //_hashSet.Clear();
                //Console.WriteLine("After Clear(): Count = " + _hashSet.Count);

                // UnionWith
                var other = new HashSet<T> { (T)(object)3!, (T)(object)4!, (T)(object)5! };
                _hashSet.UnionWith(other);
                Console.WriteLine($"After UnionWith: {string.Join(", ", _hashSet)}");

                // IntersectWith
                var another = new HashSet<T> { (T)(object)3!, (T)(object)5! };
                _hashSet.IntersectWith(another);
                Console.WriteLine($"After IntersectWith: {string.Join(", ", _hashSet)}");

                // ExceptWith
                _hashSet.ExceptWith(new HashSet<T> { (T)(object)3! });
                Console.WriteLine($"After ExceptWith(3): {string.Join(", ", _hashSet)}");

                // SymmetricExceptWith
                _hashSet.SymmetricExceptWith(new HashSet<T> { (T)(object)1!, (T)(object)6! });
                Console.WriteLine($"After SymmetricExceptWith: {string.Join(", ", _hashSet)}");

                // IsSubsetOf / IsSupersetOf / Overlaps / SetEquals
                Console.WriteLine($"IsSubsetOf: {_hashSet.IsSubsetOf(new HashSet<T> { (T)(object)1!, (T)(object)6! })}");
                Console.WriteLine($"IsSupersetOf: {_hashSet.IsSupersetOf(new HashSet<T> { (T)(object)1! })}");
                Console.WriteLine($"Overlaps: {_hashSet.Overlaps(new HashSet<T> { (T)(object)1!, (T)(object)7! })}");
                Console.WriteLine($"SetEquals: {_hashSet.SetEquals(new HashSet<T> { (T)(object)1!, (T)(object)6! })}");

               
            }
        
    }

}
