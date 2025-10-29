using Ingestion.Pipeline.Reports;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using IngestionPipeline.Models;
namespace Ingestion.Pipeline.Reports;


/// <summary>
/// Writes objects to XML via XmlSerializer. Sealed to lock serialization contract.
/// </summary>
public sealed class XmlReportWriter: IReportWriter<Report>
{
    public void WriteReport(Report report, string path)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(path) ?? ".");
        var serializer = new XmlSerializer(typeof(Report));
        using var fs = File.Create(path);
        serializer.Serialize(fs, report);
    }
}
