using LibraryCheckInDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace IngestionPipeline.Models
{
    public class SummaryReport
    {
        public int TotalFilesProcessed { get; set; }
        public int TotalBooks { get; set; }

        [XmlIgnore]
        public Dictionary<string, int> ConditionCount { get; set; }

        [JsonIgnore]
        [XmlArray("ConditionCount")]
        [XmlArrayItem("Condition")]
        public List<SerializableKeyValue> BookConditionCounts { get; set; }
        public List<Book> TopBooksBooks { get; set; }
    }


}
