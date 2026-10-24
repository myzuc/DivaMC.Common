using System.Text.Json.Nodes;

namespace Net.Myzuc.DivaMC.Common.Game.Chat.Components
{
   
    public sealed record TextChatComponent : ChatComponent
    {
        protected override string Type => "text";
        public string Text { get; init; } = string.Empty;
        
        public TextChatComponent()
        {
            
        }
        public TextChatComponent(string text)
        {
            Text = text;
        }
        internal TextChatComponent(JsonObject obj) : base(obj)
        {
            Text = obj["text"]?.AsValue().GetValue<string>() ?? throw new ArgumentException("Missing member!");
        }

        public override JsonNode ToJSON()
        {
            JsonObject json = base.ToJSON().AsObject();
            if (json.Count == 1 && json.TryGetPropertyValue("extra", out JsonNode? children))
            {
                return children!;
            }
            if (json.Count == 0)
            {
                return Text;
            }
            json["text"] = Text;
            return json;
        }

        internal static bool TryFromJSON(JsonObject obj, out TextChatComponent? chat)
        {
            chat = null;
            if (obj.ContainsKey("text")) return false;
            chat = new TextChatComponent(obj);
            return true;
        }
    }
}