using System.Data;
using LibraryCheckInDomain;
using LibraryCheckInIO;
namespace LibraryCheckIn
{
    internal class Program
    {
     
        static void Main(string[] args)
        {

            try
            {
                Console.WriteLine("Library Check-In Utility");
                Console.WriteLine();
                string inputFile = GetInputFilePath(args);
                Console.WriteLine($"Processing file: {inputFile}");
                var dataTable = CsvParser.ParseToDataTable(inputFile);
                var books = CsvParser.MapToBooks(dataTable).ToList();
                Console.WriteLine($"Successfully parsed {books.Count} book returns");

               
                var summary = new DailySummary(books, DateTime.Now);
                var outputPath = ReportGenerator.WriteDailySummary(summary);

                Console.WriteLine($"Daily summary written to: {outputPath}");
                Console.WriteLine();
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(Directory.GetCurrentDirectory());
                Console.WriteLine($"Error: File not found - {ex.Message}");
                Console.WriteLine("Please ensure the CSV file exists and the path is correct.");
               
            }
            catch (InvalidDataException ex)
            {
                Console.WriteLine($"Error: Invalid data format - {ex.Message}");
                Console.WriteLine("Please check your CSV file format and content.");
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                Console.WriteLine("Please contact support if this issue persists.");
                
            }

        }
        private static string GetInputFilePath(string[] args)
        {
            if (args.Length > 0)
            {
                return args[0];
            }
            var today = DateTime.Today.ToString("yyyyMMdd");
            return $"returns_{today}.csv";
        }
        static void PrintTable(DataTable table)
        {
            foreach (DataColumn col in table.Columns)
            {
                Console.Write(col.ColumnName + "\t");
            }
            Console.WriteLine();
            foreach (DataRow row in table.Rows)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    Console.Write(row[i] + "\t");
                }
                Console.WriteLine();
            }

        }

    }
}
