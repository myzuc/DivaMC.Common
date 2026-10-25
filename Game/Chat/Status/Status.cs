using System.Text.Json.Serialization;
using DivaMC.Common.Game.Chat.Components;

namespace DivaMC.Common.Game.Chat.Status
{
    public sealed record Status
    {
        [JsonPropertyName("version")]
        public VersionInfo? Version { get; init; } = null;
        [JsonPropertyName("players")]
        public PlayersInfo? Players { get; init; } = null;
        [JsonPropertyName("description")]
        public ChatComponent? Description { get; init; } = null;
        [JsonPropertyName("favicon")]
        public string? Icon { get; init; } = null;
        [JsonPropertyName("enforcesSecureChat")]
        public bool? HasCensorship { get; init; } = null;
        public Status()
        {
            
        }
    }
}