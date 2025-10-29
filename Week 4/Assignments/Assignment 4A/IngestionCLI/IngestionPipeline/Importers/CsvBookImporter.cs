using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using LibraryCheckInDomain;

namespace Ingestion.Pipeline.Importers
{
    /// <summary>
    /// Imports Book data from a CSV file.
    /// This class is sealed because it provides a complete and specific implementation for CSV parsing
    /// </summary>
    public sealed class CsvBookImporter : FileImporter<Book>
    {
        public override IEnumerable<Book> Import(string path)
        {
            var lines = File.ReadAllLines(path);
            for (int i = 1; i < lines.Length; i++)
            {
                var columns = lines[i].Split(',');
                if (columns.Length == 4)
                {
                    int Id = int.Parse(columns[0]);
                    string Title = columns[1];
                    string Author = columns[2];
                    if (!Enum.TryParse(columns[3], out BookCondition Condition))
                    {
                        throw new InvalidDataException($"Invalid book condition");

                    }
                    yield return new Book(Id, Title, Author, Condition);

                }
                else
                {
                           throw new InvalidDataException($"Invalid data format in line {i + 1}");
                }
            }
        }
    }
}

