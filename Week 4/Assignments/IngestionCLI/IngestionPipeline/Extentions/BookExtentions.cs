using LibraryCheckInDomain;
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Generic extension methods for enumerable of Book.
/// </summary>
public static class BookExtensions
{
    /// <summary>
    /// Return top N books ordered by keySelector descending.
    /// TValue must be comparable (Comparer&lt;TValue&gt;.Default is used).
    /// </summary>
    public static IEnumerable<Book> TopBy<TValue>(this IEnumerable<Book> source, Func<Book, TValue> keySelector, int n)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));
        if (n <= 0) return Enumerable.Empty<Book>();

        return source.OrderByDescending(keySelector).Take(n);
    }

    /// <summary>
    /// Returns counts grouped by Condition
    /// </summary>
    public static IDictionary<string, int> ToConditionCounts(this IEnumerable<Book> source)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));

      return source.GroupBy(b=>b.Condition)
            .ToDictionary(g=>g.Key.ToString(),g=>g.Count());
       
    }
}
