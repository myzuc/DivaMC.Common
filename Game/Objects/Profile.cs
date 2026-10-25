using System.Text.Json.Serialization;
using DivaMC.Common.IO;
using DivaMC.Common.IO.JSON.JsonConverters;
using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.Game.Objects
{
    public sealed record Profile : IBinarySerializable<Profile>
    {
        [JsonConverter(typeof(StringGuidJsonConverter))]
        [JsonPropertyName("id")]
        public required Guid Guid { get; set; }
        [JsonPropertyName("name")]
        public required string Name { get; set; }
        [JsonPropertyName("properties")]
        public List<Property> Properties { get; set; } = [];
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
            int properties = stream.ReadVS32();
            for (int i = 0; i < properties; i++)
            {
                data.Properties[i] = stream.Read<Property>();
            }
            return data;
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