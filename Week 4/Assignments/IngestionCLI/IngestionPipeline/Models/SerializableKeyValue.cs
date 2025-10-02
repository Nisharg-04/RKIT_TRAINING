using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngestionPipeline.Models
{
    public class SerializableKeyValue
    {
        public string Key { get; set; } = string.Empty;
        public int Value { get; set; }
    }
}
