using System.Collections.Immutable;
using System.Drawing;
using System.Globalization;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DivaMC.Common.Game.Chat.Components
{
    [JsonConverter(typeof(ChatComponentJsonConverter))]
    public abstract record ChatComponent
    {
        protected abstract string Type { get; }
        public ImmutableList<ChatComponent>? Children { get; init; } = null;
        public Color? Color { get; init; } = null;
        public Color? ShadowColor { get; init; } = null;
        public bool? Bold { get; init; } = null;
        public bool? Italic { get; init; } = null;
        public bool? Underlined { get; init; } = null;
        public bool? Strikethrough { get; init; } = null;
        public bool? Obfuscated { get; init; } = null;
        public string? Font { get; init; } = null;
        
        protected internal ChatComponent()
        {
            
        }
        protected internal ChatComponent(JsonObject obj)
        {
            if (obj.TryGetPropertyValue("extra", out JsonNode? children))
            {
                if (children is not JsonArray list) throw new InvalidDataException("Invalid format!");
                Children = list!.Select<JsonNode, ChatComponent>(node => FromJSON(node.GetValue<string>())).ToImmutableList();
            }
            if (obj.TryGetPropertyValue("color", out JsonNode? color))
            {
                Color = DecodeColor(color!.AsValue().ToString());
            }
            if (obj.TryGetPropertyValue("shadow_color", out JsonNode? shadowColor))
            {
                ShadowColor = System.Drawing.Color.FromArgb(shadowColor!.GetValue<int>());
            }
            if (obj.TryGetPropertyValue("bold", out JsonNode? bold))
            {
                Bold = bold!.GetValue<bool>();
            }
            if (obj.TryGetPropertyValue("italic", out JsonNode? italic))
            {
                italic = italic!.GetValue<bool>();
            }
            if (obj.TryGetPropertyValue("underlined", out JsonNode? underlined))
            {
                Underlined = underlined!.GetValue<bool>();
            }
            if (obj.TryGetPropertyValue("strikethrough", out JsonNode? strikethrough))
            {
                Strikethrough = strikethrough!.GetValue<bool>();
            }
            if (obj.TryGetPropertyValue("obfuscated", out JsonNode? obfuscated))
            {
                Obfuscated = obfuscated!.GetValue<bool>();
            }
            if (obj.TryGetPropertyValue("font", out JsonNode? font))
            {
                Font = font!.GetValue<string>();
            }
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
            return obj;
        }
        public static ChatComponent FromJSON(JsonNode? json)
        {
            switch (json)
            {
                case null: throw new InvalidDataException("Invalid format!");
                case JsonValue value: return value.ToString();
                case JsonArray list: return list!.Select<JsonNode, ChatComponent>(node => FromJSON(node.GetValue<string>())).ToImmutableList();
                case JsonObject obj:
                {
                    if (obj.TryGetPropertyValue("type", out JsonNode? type))
                    {
                        return type!.GetValue<string>() switch
                        {
                            "text" => new TextChatComponent(obj),
                            //
                            _ => throw new InvalidDataException("Invalid format!")
                        };
                    }
                    if (TextChatComponent.TryFromJSON(obj, out TextChatComponent? text)) return text!;
                    //
                    throw new InvalidDataException("Unrecognized format!");
                }
                default: throw new InvalidDataException("Unrecognized format!");
            }
        }
        
        public static implicit operator ChatComponent(string data)
        {
            return new TextChatComponent(data);
        }
        public static implicit operator ChatComponent(string[] data)
        {
            return new TextChatComponent()
            {
                Children = data.Select<string, ChatComponent>(child => new TextChatComponent(child)).ToImmutableList(),
            };
        }
        public static implicit operator ChatComponent(ChatComponent[] data)
        {
            return new TextChatComponent()
            {
                Children = data.ToImmutableList(),
            };
        }
        public static implicit operator ChatComponent(ImmutableList<ChatComponent> data)
        {
            return new TextChatComponent()
            {
                Children = data,
            };
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