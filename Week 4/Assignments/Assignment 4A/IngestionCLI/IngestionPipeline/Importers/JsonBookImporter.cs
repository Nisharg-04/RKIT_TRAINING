
using LibraryCheckInDomain;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ingestion.Pipeline.Importers
{
    /// <summary>
    /// Imports Books from JSON files. Accepts single object or array.
    /// Sealed because this is a final concrete implementation.
    /// </summary>
    public sealed class JsonBookImporter : FileImporter<Book>
    {
        public override IEnumerable<Book> Import(string path)
        {
            if (!File.Exists(path)) throw new FileNotFoundException(path);
            var json = File.ReadAllText(path);
            
            if (string.IsNullOrWhiteSpace(json)) yield break;
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters =
                    {
                        new JsonStringEnumConverter()
                    }
            };
            Book[]? arr = null;
            arr = JsonSerializer.Deserialize<Book[]>(json, options);
            if (arr != null)
            {
                foreach (var b in arr)
                    yield return b;
                yield break;
            }



        }
    }
}