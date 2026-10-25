using System.Text.Json.Serialization;
using DivaMC.Common.Game.Chat.Components;

namespace DivaMC.Common.Game.Chat.Status
{
    public sealed record Status
    {
        [JsonPropertyName("version")]
        public VersionInfo? Version { get; set; } = null;
        [JsonPropertyName("players")]
        public PlayersInfo? Players { get; set; } = null;
        [JsonPropertyName("description")]
        public ChatComponent? Description { get; set; } = null;
        [JsonPropertyName("favicon")]
        public string? Icon { get; set; } = null;
        [JsonPropertyName("enforcesSecureChat")]
        public bool? HasCensorship { get; set; } = null;
        public Status()
        {
            
        }
    }
}