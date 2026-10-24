using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Net.Myzuc.DivaMC.Common.Game.Chat.Components
{
    internal class ChatComponentJsonConverter : JsonConverter<ChatComponent>
    {
        public override ChatComponent Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return ChatComponent.FromJSON(JsonNode.Parse(ref reader));
        }
        public override void Write(Utf8JsonWriter writer, ChatComponent value, JsonSerializerOptions options)
        {
            value.ToJSON().WriteTo(writer);
        }
    }
}