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
            /// <summary>
            /// Unique identifier for the book.
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// Book Title
            /// </summary>
            public string Title { get; set; } = string.Empty;

            /// <summary>
            /// Book Author
            /// </summary>
            public string Author { get; set; } = string.Empty;

            /// <summary>
            /// Book Condition
            /// </summary>
            public BookCondition Condition { get; set; }

            /// <summary>
            /// Needed by XmlSerializer (parameterless constructor)
            /// </summary>
            public Book() { }

            /// <summary>
            /// Create a new book instance. Parameterized Constructor
            /// </summary>
            public Book(int id, string title, string author, BookCondition condition)
            {
                if (id <= 0)
                    throw new ArgumentException("Book ID must be positive", nameof(id));
                if (string.IsNullOrWhiteSpace(title))
                    throw new ArgumentException("Title cannot be null or empty", nameof(title));
                if (string.IsNullOrWhiteSpace(author))
                    throw new ArgumentException("Author cannot be null or empty", nameof(author));

                Id = id;
                Title = title;
                Author = author;
                Condition = condition;
            }

            /// <summary>
            /// Returns formatted string summary of the book
            /// </summary>
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





