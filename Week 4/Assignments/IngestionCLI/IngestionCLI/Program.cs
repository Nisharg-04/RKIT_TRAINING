using Ingestion.Pipeline.Importers;
using Ingestion.Pipeline.Reports;
using LibraryCheckInDomain;
using System.Text.Json;
using System.Xml.Serialization;
using IngestionPipeline.Models;
namespace IngestionCLI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var inputDir = Path.Combine(Directory.GetCurrentDirectory(), "in");
            var outDir = Path.Combine(Directory.GetCurrentDirectory(), "out");
            bool dryRun = args.Contains("--dry-run");
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "--in" && i + 1 < args.Length) inputDir = args[++i];
                else if (args[i] == "--out" && i + 1 < args.Length) outDir = args[++i];
                else if (args[i] == "--dry-run") dryRun = true;
            }

            Console.WriteLine($"Scanning: {inputDir}");
            var supported = new[] { ".csv", ".json" };
            //var files = Directory.Exists(inputDir)
            //            ? Directory.EnumerateFiles(inputDir, "*.*", SearchOption.AllDirectories)
            //            .Where(f => supported.Contains(Path.GetExtension(f)))
            //             .ToList()
            //             : new List<string>();

            DirectoryInfo di = new DirectoryInfo(inputDir); 
            var jsonFiles=di.Exists ? di.GetFiles("*.json", SearchOption.AllDirectories).Select(f => f.FullName) : Array.Empty<string>();
            var csvFiles=di.Exists ? di.GetFiles("*.csv", SearchOption.AllDirectories).Select(f => f.FullName) : Array.Empty<string>();
            var files = jsonFiles.Concat(csvFiles).ToList();

            if (dryRun)
            {
                PerformDryRun(files);
                return;
            }


            Console.WriteLine($"Found {files.Count} supported files.");
            var allBooks = new List<Book>();
            int filesProcessed = 0;

            foreach (var f in files)
            {
                try
                {
                    IEnumerable<Book> imported;
                    var ext = Path.GetExtension(f).ToLowerInvariant();
                    if (ext == ".csv")
                        imported = new CsvBookImporter().Import(f);
                    else // .json
                        imported = new JsonBookImporter().Import(f);

                    var items = imported.ToList();
                    filesProcessed++;
                    Console.WriteLine($"Imported {items.Count} books from {f}");
                    allBooks.AddRange(items);
                }
                catch (Exception ex)
                {
                   Console. WriteLine($"ERROR importing {f}: {ex.Message}");
                }

            }
            if (allBooks.Any())
            {

                var conditionCounts = allBooks.ToConditionCounts();
                var report = new Report
                {
                    GeneratedAt = DateTime.Now,
                    TotalFilesProcessed = filesProcessed,
                    TotalBooks = allBooks.Count,
                    AllBooks = allBooks

                };
                // reports
                new TextReportWriter().WriteReport(report, Path.Combine(outDir, "full_report.txt"));
                new XmlReportWriter().WriteReport(report, Path.Combine(outDir, "full_report.xml"));
                Console.WriteLine("Generated full_report.txt and full_report.xml");


                var topBooks = allBooks.TopBy(b => b.Id, 5);

                var summary = new SummaryReport
                {
                    TotalFilesProcessed = filesProcessed,
                    TotalBooks = allBooks.Count,
                    ConditionCount = conditionCounts.ToDictionary<string,int>(),
                    BookConditionCounts = conditionCounts.Select(kv => new SerializableKeyValue { Key=kv.Key,Value=kv.Value}).ToList(),
                    TopBooksBooks = topBooks.ToList()
                };
                var jsonPath = Path.Combine(outDir, $"summary_{DateTime.Now:yyyyMMdd_HHmmss}.json");
                File.WriteAllText(jsonPath, JsonSerializer.Serialize(summary, new JsonSerializerOptions { WriteIndented = true }));

                var xmlPath = Path.Combine(outDir, $"summary_{DateTime.Now:yyyyMMdd_HHmmss}.xml");
                var xmlserilizer = new XmlSerializer(typeof(SummaryReport));
                using var fs = File.Create(xmlPath);
                xmlserilizer.Serialize(fs, summary);
                Console.WriteLine($"Serialized summary to {jsonPath} and {xmlPath}");


            }

        }
        private static void PerformDryRun(List<string> files)
        {
            Console.WriteLine("\n--- DRY RUN MODE ---");
            Console.WriteLine("The following actions would be performed:");
            if (!files.Any())
            {
                Console.WriteLine(" - No supported files found in the './in' directory.");
                return;
            }

            Console.WriteLine($" - Scan './in' directory recursively.");
            Console.WriteLine($" - Found {files.Count} files to import:");
            foreach (var file in files)
            {
                Console.WriteLine($"   - {Path.GetFileName(file)}");
            }
            Console.WriteLine(" - Aggregate all imported books.");
            Console.WriteLine(" - Generate reports in './out' directory.");
            Console.WriteLine(" - Analyze data to find condition counts.");
            Console.WriteLine(" - Serialize analysis summary  to './out' directory.");
            Console.WriteLine("--- END DRY RUN ---");
        }


    }
}
