using System.Collections.Immutable;
using DivaMC.Common.IO.Serialization;
using DivaMC.Common.Primitives;

namespace DivaMC.Common.Game.Registries
{
    public record Registry : IBinarySerializable<Registry>
    {
        public static readonly Identifier RegistryName = "minecraft:root";

        public static readonly IImmutableList<Registry> StubRegistries =
        [
            (Registry)DimensionType.StubRegistry,
            (Registry)Variants.EntitySounds.CatSounds.StubRegistry,
            (Registry)Variants.EntitySounds.ChickenSounds.StubRegistry,
            (Registry)Variants.EntitySounds.CowSounds.StubRegistry,
            (Registry)Variants.EntitySounds.PigSounds.StubRegistry,
            (Registry)Variants.EntitySounds.WolfSounds.StubRegistry,
            (Registry)Variants.Entities.CatVariant.StubRegistry,
            (Registry)Variants.Entities.ChickenVariant.StubRegistry,
            (Registry)Variants.Entities.CowVariant.StubRegistry,
            (Registry)Variants.Entities.FrogVariant.StubRegistry,
            (Registry)Variants.Entities.PigVariant.StubRegistry,
            (Registry)Variants.Entities.WolfVariant.StubRegistry,
            (Registry)Variants.Entities.ZombieNautilusVariant.StubRegistry,
            (Registry)Variants.Entities.PaintingVariant.StubRegistry,
            (Registry)DamageType.StubRegistry,
            (Registry)Trims.TrimMaterial.StubRegistry,
            (Registry)Trims.TrimPattern.StubRegistry,
            (Registry)BlockTransformer.StubRegistry,
        ];
        
        public required Identifier Id { get; init; }
        public required IImmutableList<RegistryEntry> Entries { get; init; }
        public Registry()
        {
            
        }

        public int IdOf(Identifier id)
        {
            int index = Entries.Select((entry, i) => (entry, i)).First(pair => pair.entry.Id == id).i;
            //int index = Entries.FindIndex(entry => entry.Id == id);
            if (index < 0) throw new ArgumentException("Entry not found!");
            return index;
        }
        public Identifier IdOf(int id)
        {
            return EntryOf(id).Id;
        }
        public RegistryEntry EntryOf(Identifier id)
        {
            RegistryEntry? entry = Entries.FirstOrDefault(entry => entry.Id == id);
            if (entry is null) throw new ArgumentException("Entry not found!");
            return entry;
        }
        public RegistryEntry EntryOf(int id)
        {
            RegistryEntry? entry = Entries.ElementAtOrDefault(id);
            if (entry is null) throw new ArgumentException("Entry not found!");
            return entry;
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
        
        public int IdOf(Identifier id)
        {
            int index = Entries.Select((entry, i) => (entry, i)).First(pair => pair.entry.Id == id).i;
            //int index = Entries.FindIndex(entry => entry.Id == id);
            if (index < 0) throw new ArgumentException("Entry not found!");
            return index;
        }
        public Identifier IdOf(int id)
        {
            return EntryOf(id).Id;
        }
        public RegistryEntry<T> EntryOf(Identifier id)
        {
            RegistryEntry<T>? entry = Entries.FirstOrDefault(entry => entry.Id == id);
            if (entry is null) throw new ArgumentException("Entry not found!");
            return (RegistryEntry<T>)entry;
        }
        public RegistryEntry<T> EntryOf(int id)
        {
            RegistryEntry<T>? entry = Entries.ElementAtOrDefault(id);
            if (entry is null) throw new ArgumentException("Entry not found!");
            return (RegistryEntry<T>)entry;
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