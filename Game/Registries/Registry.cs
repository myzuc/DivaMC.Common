using System.Collections.Immutable;
using DivaMC.Common.IO.Serialization;
using DivaMC.Common.Primitives;

namespace DivaMC.Common.Game.Registries
{
    public record Registry : IBinarySerializable<Registry>
    {
        public required Identifier Id { get; init; }
        public required IImmutableList<RegistryEntry> Entries { get; init; }
        public Registry()
        {
            
        }
        
        public void Serialize(Stream stream)
        {
            stream.WriteT0AVS32(Id);
            stream.WriteVS32(Entries.Count);
            foreach (RegistryEntry entry in Entries)
            {
                stream.Write(entry);
            }
        }
        public static Registry Deserialize(Stream stream)
        {
            return new()
            {
                Id = stream.Read<Identifier>(),
                Entries = Enumerable.Range(0, stream.ReadVS32()).Select(i => stream.Read<RegistryEntry>()).ToImmutableArray(),
            };
        }
    }
    public record Registry<T> : IBinarySerializable<Registry<T>> where T : class, INbtSerializable<T>
    {
        public required Identifier Id { get; init; }
        public required IImmutableList<RegistryEntry<T>> Entries { get; init; }
        public Registry()
        {
            
        }
        
        public void Serialize(Stream stream)
        {
            stream.WriteT0AVS32(Id);
            stream.WriteVS32(Entries.Count);
            foreach (RegistryEntry<T> entry in Entries)
            {
                stream.Write(entry);
            }
        }
        public static Registry<T> Deserialize(Stream stream)
        {
            return new()
            {
                Id = stream.Read<Identifier>(),
                Entries = Enumerable.Range(0, stream.ReadVS32()).Select(i => stream.Read<RegistryEntry<T>>()).ToImmutableArray(),
            };
        }
        
        public static explicit operator Registry(Registry<T> data)
        {
            return new()
            {
                Id = data.Id,
                Entries = data.Entries.Select(entry => (RegistryEntry)entry).ToImmutableArray(),
            };
        }
        public static explicit operator Registry<T>(Registry data)
        {
            return new()
            {
                Id = data.Id,
                Entries = data.Entries.Select(entry => (RegistryEntry<T>)entry).ToImmutableArray(),
            };
        }
    }
}