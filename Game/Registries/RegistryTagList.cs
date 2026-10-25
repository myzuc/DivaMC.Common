using System.Collections.Immutable;
using DivaMC.Common.IO.Serialization;
using DivaMC.Common.Primitives;

namespace DivaMC.Common.Game.Registries
{
    public sealed record RegistryTagList : IBinarySerializable<RegistryTagList>
    {
        public required Identifier Id { get; init; }
        public required IImmutableList<RegistryTagListEntry> Entries { get; init; }
        public RegistryTagList()
        {
            
        }
        
        public void Serialize(Stream stream)
        {
            stream.WriteT0AVS32(Id);
            stream.WriteVS32(Entries.Count);
            foreach (RegistryTagListEntry entry in Entries)
            {
                stream.Write(entry);
            }
        }
        public static RegistryTagList Deserialize(Stream stream)
        {
            return new()
            {
                Id = stream.Read<Identifier>(),
                Entries = Enumerable.Range(0, stream.ReadVS32()).Select(i => stream.Read<RegistryTagListEntry>()).ToImmutableArray(),
            };
        }
    }
}