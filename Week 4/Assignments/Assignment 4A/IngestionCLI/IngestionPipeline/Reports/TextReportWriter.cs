using Ingestion.Pipeline.Reports;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IngestionPipeline.Models;

namespace Ingestion.Pipeline.Reports;
/// <summary>
/// Writes a simple text report listing items. Sealed because we want a stable output format.
/// </summary>

public sealed class TextReportWriter : IReportWriter<Report>
{
    public  void WriteReport(Report report, string path)
    {
        var sb = new StringBuilder();

         sb.AppendLine($"Processing Time: {report.GeneratedAt}");

        sb.AppendLine();

        sb.AppendLine($"Total Files Processed: {report.TotalFilesProcessed}");
        sb.AppendLine();

        sb.AppendLine($"Total Books Processed: {report.TotalBooks}");

        sb.AppendLine();

        foreach (var it in report.AllBooks)
        {
            sb.AppendLine(it?.ToString() ?? string.Empty);
        }
        Directory.CreateDirectory(Path.GetDirectoryName(path) ?? ".");
        File.WriteAllText(path, sb.ToString());
    }
}
