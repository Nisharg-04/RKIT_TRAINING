using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericCollectionsDemo
{
    public class ListMethodsDemo
    {
        private List<int> _list;

        public ListMethodsDemo()
        {
            _list = new List<int>() { 10, 20, 30, 40, 50 };
        }

        public void ShowMethods()
        {
            Console.WriteLine("Methods of List<int>");


            _list.Add(60);
            _list.AddRange(new int[] { 70, 80 });

            
            Console.WriteLine("BinarySearch(30): " + _list.BinarySearch(30));

          
            Console.WriteLine("Contains(20): " + _list.Contains(20));

        
            int[] arr = new int[_list.Count];
            _list.CopyTo(arr);

            Console.WriteLine("Copied Array: " + string.Join(", ", arr));

           
            Console.WriteLine("Find(x > 25): " + _list.Find(x => x > 25));
            var all = _list.FindAll(x => x > 25);
            Console.WriteLine("FindAll(x > 25): " + string.Join(", ", all));

            
            Console.WriteLine("FindIndex(x > 25): " + _list.FindIndex(x => x > 25));
            Console.WriteLine("FindLastIndex(x > 25): " + _list.FindLastIndex(x => x > 25));

            
            _list.ForEach(item => Console.WriteLine("ForEach item: " + item));

           
            var subList = _list.GetRange(1, 3);
            Console.WriteLine("GetRange(1, 3): " + string.Join(", ", subList));

            // Inserting
            _list.Insert(0, 5);
            _list.InsertRange(2, new int[] { 15, 17 });

            // Searching last
            Console.WriteLine("LastIndexOf(20): " + _list.LastIndexOf(20));

            // Removing
            _list.Remove(15);
            _list.RemoveAll(x => x > 50);
            _list.RemoveAt(0);
            _list.RemoveRange(0, Math.Min(1, _list.Count));

            // Reversing
            _list.Reverse();

            // Sorting
            _list.Sort();

            // Conversion
            var array = _list.ToArray();
            Console.WriteLine("ToArray(): " + string.Join(", ", array));

            _list.TrimExcess();

        }
    }

}
