using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Secure_Notes_Vault.Utils
{
    public class DateTimeOffsetConverter : JsonConverter<DateTimeOffset>
    {
        public override DateTimeOffset Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            // Read the string from the JSON and parse it
            return DateTimeOffset.Parse(reader.GetString() ?? string.Empty);
        }

        public override void Write(
            Utf8JsonWriter writer,
            DateTimeOffset value,
            JsonSerializerOptions options)
        {
            // Write the value as a string in "o" (round-trip) format
            writer.WriteStringValue(value.ToString("o"));
        }
    }
}
