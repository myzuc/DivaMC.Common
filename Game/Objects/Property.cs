using System.Text.Json.Serialization;
using DivaMC.Common.IO;
using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.Game.Objects
{
    public sealed record Property : IBinarySerializable<Property>
    {
        [JsonPropertyName("name")]
        public required string Name { get; init; }
        [JsonPropertyName("value")]
        public required string Value { get; init; }
        [JsonPropertyName("signature")]
        public string? Signature { get; init; }
        public Property()
        {
            
        }
        
        static Property IBinarySerializable<Property>.Deserialize(Stream stream)
        {
            return new()
            {
                Name = stream.ReadT0AVS32(),
                Value = stream.ReadT0AVS32(),
                Signature = stream.ReadU1() ? stream.ReadT0AVS32() : null
            };
        }
        void IBinarySerializable<Property>.Serialize(Stream stream)
        {
            stream.WriteT0AVS32(Name);
            stream.WriteT0AVS32(Value);
            stream.WriteU1(Signature is not null);
            if (Signature is not null)
            {
                stream.WriteT0AVS32(Signature);
            }
        }
    }
}