using System.Text.Json.Nodes;
using DivaMC.Common.IO.Serialization;
using fNbt;

namespace DivaMC.Common.Chat.Components
{
   
    public sealed record TextChatComponent : ChatComponent
    {
        public static readonly string Type = "text";
        public string Text { get; set; } = string.Empty;
        
        public TextChatComponent() : base(Type)
        {
            
        }
        public TextChatComponent(string text) : base(Type)
        {
            Text = text;
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
    }
}