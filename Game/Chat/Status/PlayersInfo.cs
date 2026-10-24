using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace DivaMC.Common.Game.Chat.Status
{
    public sealed record PlayersInfo
    {
        [JsonPropertyName("max")]
        public required int Maximum { get; init; }
        [JsonPropertyName("online")]
        public required int Online { get; init; }
        [JsonPropertyName("sample")] 
        public ImmutableList<PlayerSample>? Samples { get; init; } = null;
        public PlayersInfo()
        {
            
        }
    }
}