using System.Collections.Immutable;
using DivaMC.Common.IO.Serialization;
using DivaMC.Common.Primitives;

namespace DivaMC.Common.Game.Registries
{
    public sealed record RegistryTagListEntry : IBinarySerializable<RegistryTagListEntry>
    {
        public Identifier TagId { get; }
        public IImmutableList<int> EntryIds { get; }
        public RegistryTagListEntry(Identifier tagId,  IImmutableList<int> value)
        {
            TagId = tagId;
            EntryIds = value;
        }
        
        public void Serialize(Stream stream)
        {
            stream.Write(TagId);
            stream.WriteVS32(EntryIds.Count);
            foreach (int entryId in EntryIds)
            {
                stream.WriteVS32(entryId);
            }
        }
        public static RegistryTagListEntry Deserialize(Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}