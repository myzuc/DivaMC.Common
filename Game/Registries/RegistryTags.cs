using DivaMC.Common.IO.Serialization;
using DivaMC.Common.Primitives;

namespace DivaMC.Common.Game.Registries
{
    public sealed class RegistryTags : IBinarySerializable<RegistryTags>
    {
        public Identifier Id { get; set; }
        public Dictionary<Identifier, List<int>> Tags { get; set; } = [];
        public RegistryTags(Identifier id)
        {
            Id = id;
        }
        
        public void Serialize(Stream stream)
        {
            stream.WriteT0AVS32(Id);
            stream.WriteVS32(Tags.Count);
            foreach (KeyValuePair<Identifier, List<int>> entry in Tags)
            {
                stream.Write(entry.Key);
                stream.WriteVS32(entry.Value.Count);
                foreach (int entryId in entry.Value)
                {
                    stream.WriteVS32(entryId);
                }
            }
        }
        public static RegistryTags Deserialize(Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}