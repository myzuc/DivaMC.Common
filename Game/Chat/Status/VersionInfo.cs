using System.Text.Json.Serialization;

namespace DivaMC.Common.Game.Chat.Status
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