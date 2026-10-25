using DivaMC.Common.IO.Serialization;
using DivaMC.Common.Primitives;
using fNbt;

namespace DivaMC.Common.Game.Registries
{
    public sealed class Registry : IBinarySerializable<Registry>
    {
        public static readonly Identifier RegistryName = "minecraft:root";

        public static readonly IReadOnlyCollection<Registry> StubRegistries =
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
        public static readonly RegistryTags[] StubRegistryTags = [
            DimensionType.StubRegistryTags,
            Variants.EntitySounds.CatSounds.StubRegistryTags,
            Variants.EntitySounds.ChickenSounds.StubRegistryTags,
            Variants.EntitySounds.CowSounds.StubRegistryTags,
            Variants.EntitySounds.PigSounds.StubRegistryTags,
            Variants.EntitySounds.WolfSounds.StubRegistryTags,
            Variants.Entities.CatVariant.StubRegistryTags,
            Variants.Entities.ChickenVariant.StubRegistryTags,
            Variants.Entities.CowVariant.StubRegistryTags,
            Variants.Entities.FrogVariant.StubRegistryTags,
            Variants.Entities.PigVariant.StubRegistryTags,
            Variants.Entities.WolfVariant.StubRegistryTags,
            Variants.Entities.ZombieNautilusVariant.StubRegistryTags,
            Variants.Entities.PaintingVariant.StubRegistryTags,
            DamageType.StubRegistryTags,
            Trims.TrimMaterial.StubRegistryTags,
            Trims.TrimPattern.StubRegistryTags,
            BlockTransformer.StubRegistryTags,
        ];
        
        public Identifier Id { get; set; }
        public OrderedDictionary<Identifier, NbtTag?> Entries { get; set; } = [];
        public Registry(Identifier id)
        {
            Id = id;
        }

        public int IdOf(Identifier id)
        {
            int index = Entries.IndexOf(id);
            if (index < 0) throw new ArgumentException("Entry not found!");
            return index;
        }
        public Identifier IdOf(int index)
        {
            Identifier? id = Entries.Keys.ElementAt(index).FullKey;
            if (!id.HasValue) throw new ArgumentException("Entry not found!");
            return id.Value;
        }
        public NbtTag? EntryOf(Identifier id)
        {
            if (!Entries.TryGetValue(id, out NbtTag? entry)) throw new ArgumentException("Entry not found!");
            return entry;
        }
        public NbtTag? EntryOf(int index)
        {
            if (index < 0 || index > Entries.Count) throw new ArgumentException("Entry not found!");
            return Entries.GetAt(index).Value;
        }
        
        public void Serialize(Stream stream)
        {
            stream.WriteT0AVS32(Id);
            stream.WriteVS32(Entries.Count);
            foreach (KeyValuePair<Identifier, NbtTag?> entry in Entries)
            {
                stream.Write(entry.Key);
                stream.WriteU1(entry.Value is not null);
                if (entry.Value is not null) stream.WriteNbt(entry.Value);
            }
        }
        public static Registry Deserialize(Stream stream)
        {
            Registry registry = new(stream.Read<Identifier>());
            int entries = stream.ReadVS32();
            for (int i = 0; i < entries; i++)
            {
                Identifier id = stream.Read<Identifier>();
                NbtTag? value = stream.ReadU1() ? stream.ReadNbt() : null;
                registry.Entries.Add(id, value);
            }
            return registry;
        }
    }
    public sealed record Registry<T> : IBinarySerializable<Registry<T>> where T : class, INbtSerializable<T>
    {
        public Identifier Id { get; set; }
        public OrderedDictionary<Identifier, T?> Entries { get; init; } = [];
        public Registry(Identifier id)
        {
            Id = id;
        }
        
        public int IdOf(Identifier id)
        {
            int index = Entries.IndexOf(id);
            if (index < 0) throw new ArgumentException("Entry not found!");
            return index;
        }
        public Identifier IdOf(int index)
        {
            Identifier? id = Entries.Keys.ElementAt(index).FullKey;
            if (!id.HasValue) throw new ArgumentException("Entry not found!");
            return id.Value;
        }
        public T? EntryOf(Identifier id)
        {
            if (!Entries.TryGetValue(id, out T? entry)) throw new ArgumentException("Entry not found!");
            return entry;
        }
        public T? EntryOf(int index)
        {
            if (index < 0 || index > Entries.Count) throw new ArgumentException("Entry not found!");
            return Entries.GetAt(index).Value;
        }
        
        public void Serialize(Stream stream)
        {
            stream.WriteT0AVS32(Id);
            stream.WriteVS32(Entries.Count);
            foreach (KeyValuePair<Identifier, T?> entry in Entries)
            {
                stream.Write(entry.Key);
                stream.WriteU1(entry.Value is not null);
                if (entry.Value is not null) stream.WriteNbt(entry.Value);
            }
        }
        public static Registry<T> Deserialize(Stream stream)
        {
            Registry<T> registry = new(stream.Read<Identifier>());
            int entries = stream.ReadVS32();
            for (int i = 0; i < entries; i++)
            {
                Identifier id = stream.Read<Identifier>();
                T? value = stream.ReadU1() ? stream.ReadNbt<T>() : null;
                registry.Entries.Add(id, value);
            }
            return registry;
        }
        
        public static explicit operator Registry(Registry<T> data)
        {
            return new(data.Id)
            {
                Entries = new(data.Entries.Select(entry => new KeyValuePair<Identifier, NbtTag?>(entry.Key, entry.Value?.ToNbt())))
            };
        }
        public static explicit operator Registry<T>(Registry data)
        {
            return new(data.Id)
            {
                Entries = new(data.Entries.Select(entry => new KeyValuePair<Identifier, T?>(entry.Key, entry.Value is not null ? T.FromNbt(entry.Value) : null))),
            };
        }
    }
}