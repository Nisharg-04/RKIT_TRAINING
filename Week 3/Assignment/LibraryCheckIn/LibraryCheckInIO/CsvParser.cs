using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryCheckInDomain;

namespace LibraryCheckInIO
{
    /// <summary>
    /// Parses CSV files for library check-in data.
    /// </summary>
    public class CsvParser
    {
        ///<summary>
        /// Converts CSV file data into a DataTable
        ///</summary>
        ///<param name="filePath">File Path in string</param>
        ///<returns>DataTable containing parsed data</returns>
        ///<exception cref="FileNotFoundException">When file do not exists</exception>
        ///<exception cref="InvalidDataException">When CSV format is invalid</exception>
        public static DataTable ParseToDataTable(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("The csv file was not found.", filePath);
            }
            DataTable dt = new DataTable();
            try
            {
                using StreamReader sr = new StreamReader(filePath);
                string headerLine = sr.ReadLine();
                if (string.IsNullOrWhiteSpace(headerLine))
                {
                    throw new InvalidDataException("CSV file is empty or missing header.");
                }
                dt.Columns.Add("Id", typeof(int));
                dt.Columns.Add("Title", typeof(string));
                dt.Columns.Add("Author", typeof(string));
                dt.Columns.Add("Condition", typeof(BookCondition));
                string[] headers = headerLine.Split(',');
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    string[] fields = line.Split(',');
                    if (fields.Length != headers.Length)
                    {
                        throw new InvalidDataException("CSV file has inconsistent number of fields.");
                    }
                    DataRow row = dt.NewRow();

                    if (int.TryParse(fields[0], out int id))
                    {
                        row["ID"] = id;
                    }
                    else
                    {
                        throw new InvalidDataException($"Invalid ID value: {fields[0]}");
                    }
                    row["Title"] = fields[1];
                    row["Author"] = fields[2];
                    if (Enum.TryParse(fields[3], out BookCondition condition))
                    {
                        row["Condition"] = condition;
                    }
                    else
                    {
                        throw new InvalidDataException($"Invalid book condition: {fields[3]}");
                    }

                    dt.Rows.Add(row);
                }
            }
            catch (IOException ex)
            {
                throw new IOException("An error occurred while reading the CSV file.", ex);
            }


            return dt;
        }


        ///<summary>
        /// Maps DataTable rows to Book objects
        ///</summary>
        /// <param name="dataTable">DataTable containing book data</param>
        /// <returns>Collection of Book objects</returns>
        public static List<Book> MapToBooks(DataTable table)
        {
            List<Book> books = new List<Book>();
            foreach (DataRow row in table.Rows)
            {
                try
                {
                    int id = Convert.ToInt32(row["Id"]);
                    string author = row["Author"].ToString();
                    string title = row["Title"].ToString();
                    BookCondition condition = (BookCondition)row["Condition"];
                    Book book = new Book(id, author, title, condition);
                    books.Add(book);
                }
                catch (Exception ex)
                {
                    throw new InvalidDataException("Invalid Book Data in Datatable");
                }


            }
            return books;

        }

    }
}
