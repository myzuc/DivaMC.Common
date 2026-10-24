using System;
using System.Text.Json.Serialization;
using Net.Myzuc.DivaMC.Common.IO.JSON.JsonConverters;

namespace Net.Myzuc.DivaMC.Common.Game.Chat.Status
{
    public sealed record PlayerSample
    {
        [JsonPropertyName("name")] 
        public required string Name { get; init; } = string.Empty;
        [JsonConverter(typeof(StringGuidJsonConverter))]
        [JsonPropertyName("id")] 
        public required Guid Guid { get; init; } = Guid.Empty;
        [JsonConstructor]
        public PlayerSample()
        {
            
        }
    }
}