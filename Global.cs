using System.Text.Json;
using System.Text.Json.Serialization;
using DivaMC.Protocol.IO.JSON.JsonConverters;

namespace DivaMC.Protocol
{
    internal static class Global
    {
        public static readonly JsonSerializerOptions JsonSerializerOptions = new(JsonSerializerDefaults.General)
        {
            Converters =
            {
                new StringGuidJsonConverter(),
            },
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            ReadCommentHandling = JsonCommentHandling.Disallow,
            WriteIndented = false,
        };
    }
}