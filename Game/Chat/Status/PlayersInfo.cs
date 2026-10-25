using System.Text.Json.Serialization;

namespace DivaMC.Common.Game.Chat.Status
{
    public sealed record PlayersInfo
    {
        [JsonPropertyName("max")]
        public required int Maximum { get; set; }
        [JsonPropertyName("online")]
        public required int Online { get; set; }
        [JsonPropertyName("sample")] 
        public List<PlayerSample>? Samples { get; set; } = null;
        public PlayersInfo()
        {
            
        }
    }
}