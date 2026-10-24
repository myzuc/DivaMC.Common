using System.Text.Json.Serialization;
using DivaMC.Common.IO;
using DivaMC.Common.IO.JSON.JsonConverters;

namespace DivaMC.Common.Game.Objects
{
    public sealed record Profile : IBinarySerializable<Profile>
    {
        [JsonConverter(typeof(StringGuidJsonConverter))]
        [JsonPropertyName("id")]
        public required Guid Guid { get; init; }
        [JsonPropertyName("name")]
        public required string Name { get; init; }
        [JsonPropertyName("properties")]
        public IReadOnlyList<Property> Properties { get; init; } = [];
        public Profile()
        {
            
        }
        
        static Profile IBinarySerializable<Profile>.Deserialize(Stream stream)
        {
            Profile data = new()
            {
                Guid = stream.ReadGuid(),
                Name = stream.ReadT0AVS32(),
                Properties = []
            };
            Property[] properties = new Property[stream.ReadVS32()];
            for (int i = 0; i < properties.Length; i++)
            {
                properties[i] = stream.Read<Property>();
            }
            return data with
            {
                Properties = properties
            };
        }
        void IBinarySerializable<Profile>.Serialize(Stream stream)
        {
            stream.WriteGuid(Guid);
            stream.WriteT0AVS32(Name);
            stream.WriteVS32(Properties.Count);
            foreach(Property property in Properties)
            {
                stream.Write(property);
            }
        }
    }
}