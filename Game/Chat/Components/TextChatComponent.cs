using System.Text.Json.Nodes;
using DivaMC.Common.IO.Serialization;
using fNbt;

namespace DivaMC.Common.Game.Chat.Components
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
            if (json.Count == 2 && json.TryGetPropertyValue("extra", out JsonNode? children)) return children!;
            if (json.Count == 1) return Text;
            json["text"] = Text;
            return json;
        }
        public override NbtTag ToNbt()
        {
            NbtCompound nbt = (NbtCompound)base.ToNbt();
            if (nbt.Count == 2 && nbt.Contains("extra")) return NbtList.FromIEnumerable(Children!);
            if (nbt.Count == 1) return NbtString.FromT0A(Text);
            NbtString.FromT0A(Text).Rename("text").AddTo(nbt);
            return nbt;
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