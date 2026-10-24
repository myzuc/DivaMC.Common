using System.Text.Json.Serialization;

namespace Net.Myzuc.DivaMC.Common.Game.Chat.Status
{
    public sealed record VersionInfo
    {
        [JsonPropertyName("name")]
        public string Name { get; init; } = string.Empty;
        [JsonPropertyName("protocol")]
        public required int Protocol { get; init; }
        public VersionInfo()
        {
            
        }
    }
}