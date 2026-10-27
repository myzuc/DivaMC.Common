using DivaMC.Protocol.Common;
using DivaMC.Protocol.Data;
using DivaMC.Protocol.Data.Banners;
using DivaMC.Protocol.Data.DecoratedPots;
using DivaMC.Protocol.Data.Instruments;
using DivaMC.Protocol.Data.Jukeboxes;
using DivaMC.Protocol.Data.Trims;
using DivaMC.Protocol.Data.Variants.Entities;
using DivaMC.Protocol.Data.Variants.EntitySounds;
using DivaMC.Protocol.IO.Serialization;
using DivaMC.Protocol.World;
using DivaMC.Protocol.World.Biomes;
using DivaMC.Protocol.World.Dimensions;
using fNbt;

namespace DivaMC.Protocol.Registries
{
    public sealed class Registry : IByteSerializable<Registry>
    {
        public static readonly Identifier RegistryName = "minecraft:root";

        public static readonly IReadOnlyList<Registry> StubRegistries =
        [
            (Registry)Dimension.StubRegistry,
            (Registry)Biome.StubRegistry,
            (Registry)CatSounds.StubRegistry,
            (Registry)ChickenSounds.StubRegistry,
            (Registry)CowSounds.StubRegistry,
            (Registry)PigSounds.StubRegistry,
            (Registry)WolfSounds.StubRegistry,
            (Registry)CatVariant.StubRegistry,
            (Registry)ChickenVariant.StubRegistry,
            (Registry)CowVariant.StubRegistry,
            (Registry)FrogVariant.StubRegistry,
            (Registry)PigVariant.StubRegistry,
            (Registry)WolfVariant.StubRegistry,
            (Registry)ZombieNautilusVariant.StubRegistry,
            (Registry)PaintingVariant.StubRegistry,
            (Registry)DamageType.StubRegistry,
            (Registry)TrimMaterial.StubRegistry,
            (Registry)TrimPattern.StubRegistry,
            (Registry)BlockTransformer.StubRegistry,
            (Registry)JukeboxSong.StubRegistry,
            (Registry)BannerPattern.StubRegistry,
            (Registry)Instrument.StubRegistry,
            (Registry)DecoratedPotPattern.StubRegistry,
        ];
        public static readonly IReadOnlyList<RegistryTags> StubRegistryTags = 
        [
            Dimension.StubRegistryTags,
            Biome.StubRegistryTags,
            CatSounds.StubRegistryTags,
            ChickenSounds.StubRegistryTags,
            CowSounds.StubRegistryTags,
            PigSounds.StubRegistryTags,
            WolfSounds.StubRegistryTags,
            CatVariant.StubRegistryTags,
            ChickenVariant.StubRegistryTags,
            CowVariant.StubRegistryTags,
            FrogVariant.StubRegistryTags,
            PigVariant.StubRegistryTags,
            WolfVariant.StubRegistryTags,
            ZombieNautilusVariant.StubRegistryTags,
            PaintingVariant.StubRegistryTags,
            DamageType.StubRegistryTags,
            TrimMaterial.StubRegistryTags,
            TrimPattern.StubRegistryTags,
            BlockTransformer.StubRegistryTags,
            JukeboxSong.StubRegistryTags,
            BannerPattern.StubRegistryTags,
            Instrument.StubRegistryTags,
            DecoratedPotPattern.StubRegistryTags,
        ];
        
        public Identifier Id { get; set; }
        public OrderedDictionary<Identifier, NbtTag?> Entries { get; set; } = [];
        public Registry(Identifier id)
        {
            Id = id;
        }

        
        public int IndexOf(NbtTag? entry)
        {
            IEnumerable<(KeyValuePair<Identifier, NbtTag?> kvp, int index)> candidates = Entries.Select((kvp, index) => (kvp, index));
            int index = candidates.Any() ? candidates.FirstOrDefault(kvpi => kvpi.kvp.Value == entry).index : -1;
            if (index < 0) throw new ArgumentException("Entry not found!");
            return index;
        }
        public int IndexOf(Identifier id)
        {
            int index = Entries.IndexOf(id);
            if (index < 0) throw new ArgumentException("Entry not found!");
            return index;
        }
        public Identifier IdOf(NbtTag? entry)
        {
            Identifier? id = Entries.FirstOrDefault(kvp => kvp.Value == entry).Key;
            if (!id.HasValue) throw new ArgumentException("Entry not found!");
            return id.Value;
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
    public sealed class Registry<T> : IByteSerializable<Registry<T>> where T : class, INbtSerializable<T>
    {
        public Identifier Id { get; set; }
        public OrderedDictionary<Identifier, T?> Entries { get; init; } = [];
        public Registry(Identifier id)
        {
            Id = id;
        }
        
        public int IndexOf(T entry)
        {
            IEnumerable<(KeyValuePair<Identifier, T?> kvp, int index)> candidates = Entries.Select((kvp, index) => (kvp, index));
            int index = candidates.Any() ? candidates.FirstOrDefault(kvpi => kvpi.kvp.Value == entry).index : -1;
            if (index < 0) throw new ArgumentException("Entry not found!");
            return index;
        }
        public int IndexOf(Identifier id)
        {
            int index = Entries.IndexOf(id);
            if (index < 0) throw new ArgumentException("Entry not found!");
            return index;
        }
        public Identifier IdOf(T entry)
        {
            Identifier? id = Entries.FirstOrDefault(kvp => kvp.Value == entry).Key;
            if (!id.HasValue) throw new ArgumentException("Entry not found!");
            return id.Value;
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
            Registry registry = new(data.Id)
            {
                Entries = new(data.Entries.Select(entry => new KeyValuePair<Identifier, NbtTag?>(entry.Key, entry.Value?.ToNbt())))
            };
            return registry;
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