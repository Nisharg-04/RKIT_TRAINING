using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryCheckInDomain;

namespace LibraryCheckInIO
{
    /// <summary>
    /// Handles writing daily summary reports to files
    /// </summary>
    public class ReportGenerator
    {
        /// <summary>
        /// Writes daily summary to output file
        /// </summary>
        /// <param name="summary">Daily summary to write</param>
        /// <param name="outputDirectory">Output directory path</param>
        /// <returns>Path to the created file</returns>
        public static  string WriteDailySummary(DailySummary summary, string outputDirectory = "./out")
        {
           
            Directory.CreateDirectory(outputDirectory);

            var fileName = $"daily_summary_{summary.ProcessedAt:yyyyMMdd}.txt";
            var filePath = Path.Combine(outputDirectory, fileName);

            try
            {
                using var writer = new StreamWriter(filePath, false);//overwrite the existing file
                 writer.Write(summary.GenerateReport());
                return filePath;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to write report to {filePath}: {ex.Message}", ex);
            }
        }


    }
}
