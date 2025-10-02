using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericCollectionsDemo
{
    public class LinkedListMethodsDemo<T>
    {
        private LinkedList<T> _linkedList;

        public LinkedListMethodsDemo()
        {
            _linkedList = new LinkedList<T>();
        }

        public void ShowMethods()
        {
            Console.WriteLine("Methods of LinkedList<T>");

            // AddLast
            _linkedList.AddLast((T)(object)10!);
            _linkedList.AddLast((T)(object)20!);
            _linkedList.AddLast((T)(object)30!);
            Console.WriteLine("After AddLast: " + string.Join(", ", _linkedList));

            // AddFirst
            _linkedList.AddFirst((T)(object)5!);
            Console.WriteLine("After AddFirst: " + string.Join(", ", _linkedList));

            // Find
            var node = _linkedList.Find((T)(object)20!);
            Console.WriteLine($"Find(20): {node.Value}");

            // AddBefore
            if (node != null)
            {
                _linkedList.AddBefore(node, (T)(object)15!);
                Console.WriteLine("After AddBefore: " + string.Join(", ", _linkedList));
            }

            // AddAfter
            if (node != null)
            {
                _linkedList.AddAfter(node, (T)(object)25!);
                Console.WriteLine("After AddAfter: " + string.Join(", ", _linkedList));
            }

            // Remove
            _linkedList.Remove((T)(object)15!);
            Console.WriteLine("After Remove(15): " + string.Join(", ", _linkedList));

            // RemoveFirst
            _linkedList.RemoveFirst();
            Console.WriteLine("After RemoveFirst: " + string.Join(", ", _linkedList));

            // RemoveLast
            _linkedList.RemoveLast();
            Console.WriteLine("After RemoveLast: " + string.Join(", ", _linkedList));

            // Contains
            Console.WriteLine($"Contains(20): {_linkedList.Contains((T)(object)20)}");

            // Clear
            //_linkedList.Clear();
            //Console.WriteLine("After Clear: Count = " + _linkedList.Count);

        }
    }
}
