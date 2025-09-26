using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCheckInDomain
{
    /// <summary>
    /// Represents a book in the library system.
    /// </summary>
    public class Book
    {
        private readonly int _id;
        private readonly string _title;
        private readonly string _author;
        private readonly BookCondition _condition;

        /// <summary>
        ///  Unique identifier for the book.
        ///  </summary>
        public int Id { get { return _id; } }
        /// <summary>
        /// Book Title
        /// </summary>
        public string Title { get { return _title; } }

        /// <summary>
        ///  Book Author
        ///  </summary>
        public string Author { get { return _author; } }

        /// <summary>
        /// Book Condition
        /// </summary>
        
        public BookCondition Condition { get { return _condition; } }


        /// <summary>
        /// Create a new book instance. Parameterized Constructor
        /// </summary>
        /// <param name="id">Unique identifier for the book.</param>
        /// <param name="title">Book title</param>
        /// <param name="author">Book author</param>
        /// <param name="condition">Book condition</param>
        public Book(int id, string title, string author, BookCondition condition)
        {

            if (id <= 0)
                throw new ArgumentException("Book ID must be positive", nameof(id));
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be null or empty", nameof(title));
            if (string.IsNullOrWhiteSpace(author))
                throw new ArgumentException("Author cannot be null or empty", nameof(author));

            _id = id;
            _title = title;
            _author = author;
            _condition = condition;
        }
        /// <summary>
        /// Returns formatted string summary of the book
        /// </summary>
        /// <returns>Formatted string: "Title by Author"</returns>
        public string GetSummary()
        {
            return $"{Title} by {Author}";
        }

        public override string ToString()
        {
            return $"Book {Id}: {GetSummary()} - {Condition}";
        }

    }



}
