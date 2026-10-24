using System.Text.Json;
using System.Text.Json.Serialization;
using Net.Myzuc.DivaMC.Common.IO.JSON.JsonConverters;

namespace Net.Myzuc.DivaMC.Common
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