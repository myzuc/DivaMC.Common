using System.Collections.ObjectModel;
using System.Drawing;
using System.Globalization;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using DivaMC.Common.IO.Serialization;
using fNbt;

namespace DivaMC.Common.Game.Chat.Components
{
    [JsonConverter(typeof(ChatComponentJsonConverter))]
    public abstract record ChatComponent : INbtSerializable<ChatComponent>
    {
        private string Type { get; }
        public List<ChatComponent>? Children { get; set; } = null;
        public Color? Color { get; set; } = null;
        public Color? ShadowColor { get; set; } = null;
        public bool? Bold { get; set; } = null;
        public bool? Italic { get; set; } = null;
        public bool? Underlined { get; set; } = null;
        public bool? Strikethrough { get; set; } = null;
        public bool? Obfuscated { get; set; } = null;
        public string? Font { get; set; } = null;
        
        protected internal ChatComponent(string type)
        {
            Type = type;
        }

        public virtual JsonNode ToJSON()
        {
            JsonObject obj = new();
            if (Children is not null)
            {
                JsonArray list = new();
                foreach (ChatComponent chat in Children)
                {
                    list.Add(chat);
                }
                obj["extra"] = list;
            }
            if (Color.HasValue) obj["color"] = EncodeColor(Color.Value);
            if (ShadowColor.HasValue) obj["shadow_color"] = ShadowColor!.Value.ToArgb();
            if (Bold.HasValue) obj["bold"] = Bold!.Value;
            if (Italic.HasValue) obj["italic"] = Italic!.Value;
            if (Underlined.HasValue) obj["underlined"] = Underlined!.Value;
            if (Strikethrough.HasValue) obj["strikethrough"] = Strikethrough!.Value;
            if (Obfuscated.HasValue) obj["obfuscated"] = Obfuscated!.Value;
            if (Font is not null) obj["font"] = Font;
            obj["type"] = Type;
            return obj;
        }
        public static ChatComponent FromJSON(JsonNode? json)
        {
            throw new NotImplementedException();
        }
        
        public static implicit operator ChatComponent(string data)
        {
            return new TextChatComponent(data);
        }
        public static implicit operator ChatComponent(string[] data)
        {
            return new TextChatComponent()
            {
                Children = data.Select<string, ChatComponent>(child => new TextChatComponent(child)).ToList(),
            };
        }
        public static implicit operator ChatComponent(ChatComponent[] data)
        {
            return new TextChatComponent()
            {
                Children = data.ToList(),
            };
        }
        public static implicit operator ChatComponent(ReadOnlyCollection<ChatComponent> data)
        {
            return new TextChatComponent()
            {
                Children = data.ToList(),
            };
        }
        public static implicit operator ChatComponent(Collection<ChatComponent> data)
        {
            return new TextChatComponent()
            {
                Children = data.ToList(),
            };
        }
        public static implicit operator ChatComponent(List<ChatComponent> data)
        {
            return new TextChatComponent()
            {
                Children = data,
            };
        }
        
        public virtual NbtTag ToNbt()
        {
            NbtCompound nbt = new();
            if (Children is not null) NbtList.FromIEnumerable(Children).Rename("extra").AddTo(nbt);
            if (Color.HasValue) NbtString.FromT0A(EncodeColor(Color.Value)).Rename("color").AddTo(nbt);
            if (ShadowColor.HasValue) NbtInt.FromS32(ShadowColor!.Value.ToArgb()).Rename("shadow_color").AddTo(nbt);
            if (Bold.HasValue) NbtByte.FromU1(Bold!.Value).Rename("bold").AddTo(nbt);
            if (Italic.HasValue) NbtByte.FromU1(Italic!.Value).Rename("italic").AddTo(nbt);
            if (Underlined.HasValue) NbtByte.FromU1(Underlined!.Value).Rename("underlined").AddTo(nbt);
            if (Strikethrough.HasValue) NbtByte.FromU1(Strikethrough!.Value).Rename("strikethrough").AddTo(nbt);
            if (Obfuscated.HasValue) NbtByte.FromU1(Obfuscated!.Value).Rename("obfuscated").AddTo(nbt);
            if (Font is not null) NbtString.FromT0A(Font).Rename("font").AddTo(nbt);
            NbtString.FromT0A(Type).Rename("type").AddTo(nbt);
            return nbt;
        }
        public static ChatComponent FromNbt(NbtTag nbt)
        {
            //can be compound, array, string
            throw new NotImplementedException();
        }

        private static string EncodeColor(Color color)
        {
            int argb = color.ToArgb();
            return argb switch
            {
                -16777216 => "black",
                -16777046 => "dark_blue",
                -16733696 => "dark_green",
                -16733526 => "dark_aqua",
                -5636096 => "dark_red",
                -5635926 => "dark_purple",
                -22016 => "gold",
                -5592406 => "gray",
                -11184811 => "dark_gray",
                -11184641 => "blue",
                -11141291 => "green",
                -11141121 => "aqua",
                -43691 => "red",
                -43521 => "light_purple",
                -171 => "yellow",
                -1 => "white",
                _ => EncodeHex(argb & 0x00FFFFFF)
            };
        }
        private static Color DecodeColor(string color)
        {
            int argb = color switch
            {
                "black" => -16777216,
                "dark_blue" => -16777046,
                "dark_green" => -16733696,
                "dark_aqua" => -16733526,
                "dark_red" => -5636096,
                "dark_purple" => -5635926,
                "gold" => -22016,
                "gray" => -5592406,
                "dark_gray" => -11184811,
                "blue" => -11184641,
                "green" => -11141291,
                "aqua" => -11141121,
                "red" => -43691,
                "light_purple" => -43521,
                "yellow" => -171,
                "white" => -1,
                _ => DecodeHex(color)
            };
            return System.Drawing.Color.FromArgb(argb);
        }
        private static string EncodeHex(int hex)
        {
            return $"#{hex:X6}";
        }
        private static int DecodeHex(string hex)
        {
            if (!hex.StartsWith('#')) throw new InvalidDataException("Invalid Color");
            return int.Parse(hex, NumberStyles.HexNumber);
        }
    }
}