using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceGeneratorusingJson
{
    using System.Text.Json.Serialization;

    [JsonSourceGenerationOptions(
        PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
        WriteIndented = true)]
    [JsonSerializable(typeof(User))]
    internal partial class AppJsonContext : JsonSerializerContext
    {
    }

}
