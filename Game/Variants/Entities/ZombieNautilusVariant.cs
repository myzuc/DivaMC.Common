using System.Collections.Immutable;
using DivaMC.Common.Game.Objects.SpawnConditions;
using DivaMC.Common.Game.Registries;
using DivaMC.Common.IO.Serialization;
using DivaMC.Common.Primitives;
using fNbt;

namespace DivaMC.Common.Game.Variants.Entities
{
    public sealed record ZombieNautilusVariant : INbtSerializable<ZombieNautilusVariant>
    {
        public static readonly Identifier RegistryName = "minecraft:zombie_nautilus_variant";

        public static readonly ZombieNautilusVariant Stub = new()
        {
            Texture = Identifier.Empty,
            Model = "normal",
            SpawnConditions = [],
        };
        public static readonly Registry<ZombieNautilusVariant> StubRegistry = new()
        {
            Id = RegistryName,
            Entries = [new(Identifier.Empty, Stub)]
        };
        public static readonly RegistryTagList StubRegistryTags = new()
        {
            Id = RegistryName,
            Entries = []
        };
        
        public required Identifier Texture { get; init; }
        public required string Model { get; init; }
        public required IImmutableList<SpawnCondition> SpawnConditions { get; init; }

        public ZombieNautilusVariant()
        {
            
        }
        
        public NbtTag ToNbt()
        {
            NbtCompound nbt = new();
            Texture.ToNbt().Rename("asset_id").AddTo(nbt);
            NbtString.FromT0A(Model).Rename("model").AddTo(nbt);
            NbtList.FromIEnumerable(SpawnConditions).Rename("spawn_conditions").AddTo(nbt);
            return nbt;
        }
        public static ZombieNautilusVariant FromNbt(NbtTag nbt)
        {
            throw new NotImplementedException();
        }
    }
}