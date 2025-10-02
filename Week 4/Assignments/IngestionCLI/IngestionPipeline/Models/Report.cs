using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibraryCheckInDomain;
using System.Threading.Tasks;

namespace IngestionPipeline.Models
{
    public class Report
    {
        public DateTime GeneratedAt { get; set; }
        public int TotalFilesProcessed { get; set; }
        public int TotalBooks { get; set; }
        public List<Book> AllBooks { get; set; } = new List<Book>();

    }
}
