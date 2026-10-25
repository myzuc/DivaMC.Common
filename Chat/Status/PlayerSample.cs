using System.Text.Json.Serialization;
using DivaMC.Common.IO.JSON.JsonConverters;

namespace DivaMC.Common.Chat.Status
{
    public sealed record PlayerSample
    {
        [JsonPropertyName("name")] 
        public required string Name { get; set; } = string.Empty;
        [JsonConverter(typeof(StringGuidJsonConverter))]
        [JsonPropertyName("id")] 
        public required Guid Guid { get; set; } = Guid.Empty;
        [JsonConstructor]
        public PlayerSample()
        {
            
        }
    }
}