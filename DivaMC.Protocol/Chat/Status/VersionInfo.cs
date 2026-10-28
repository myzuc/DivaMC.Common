using System.Text.Json.Serialization;

namespace DivaMC.Protocol.Chat.Status
{
    public sealed record VersionInfo
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("protocol")]
        public required int Protocol { get; set; }
        public VersionInfo()
        {
            
        }
    }
}