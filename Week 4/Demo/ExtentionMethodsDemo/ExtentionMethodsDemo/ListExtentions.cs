using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtentionMethodsDemo
{
    public static class EnumerableExtensions
    {
        public static void AddRange<T>(this ICollection<T> coll, IEnumerable<T> items)
        {
            if (coll == null) throw new ArgumentNullException(nameof(coll));
            foreach (var it in items) coll.Add(it);
        }

        public static string ToCsv<T>(this IEnumerable<T> items)
        {
            return string.Join(",", items);
        }
    }

}
