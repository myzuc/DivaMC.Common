using DivaMC.Protocol.Common;
using DivaMC.Protocol.IO.Serialization;

namespace DivaMC.Protocol.Registries
{
    public sealed class RegistryTags : IByteSerializable<RegistryTags>
    {
        public Identifier Id { get; set; }
        public Dictionary<Identifier, List<int>> Entries { get; set; } = [];
        public RegistryTags(Identifier id)
        {
            Id = id;
        }
        
        public void Serialize(Stream stream)
        {
            stream.WriteT0AVS32(Id);
            stream.WriteVS32(Entries.Count);
            foreach (KeyValuePair<Identifier, List<int>> entry in Entries)
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