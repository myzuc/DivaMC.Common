using System.Collections.Immutable;
using DivaMC.Common.Game.Objects.SpawnConditions;
using DivaMC.Common.Game.Registries;
using DivaMC.Common.IO.Serialization;
using DivaMC.Common.Primitives;
using fNbt;

namespace DivaMC.Common.Game.Variants.Entities
{
    public sealed record CowVariant : INbtSerializable<CowVariant>
    {
        public static readonly Identifier RegistryName = "minecraft:cow_variant";

        public static readonly CowVariant Stub = new()
        {
            AdultTexture = Identifier.Empty,
            BabyTexture = Identifier.Empty,
            Model = "normal",
            SpawnConditions = [],
        };
        public static readonly Registry<CowVariant> StubRegistry = new()
        {
            Id = RegistryName,
            Entries = [new(Identifier.Empty, Stub)]
        };
        public static readonly RegistryTagList StubRegistryTags = new()
        {
            Id = RegistryName,
            Entries = []
        };
        
        public required Identifier AdultTexture { get; init; }
        public required Identifier BabyTexture { get; init; }
        public required string Model { get; init; }
        public required IImmutableList<SpawnCondition> SpawnConditions { get; init; }

        public CowVariant()
        {
            
        }
        
        public NbtTag ToNbt()
        {
            NbtCompound nbt = new();
            AdultTexture.ToNbt().Rename("asset_id").AddTo(nbt);
            BabyTexture.ToNbt().Rename("baby_asset_id").AddTo(nbt);
            NbtString.FromT0A(Model).Rename("model").AddTo(nbt);
            NbtList.FromIEnumerable(SpawnConditions).Rename("spawn_conditions").AddTo(nbt);
            return nbt;
        }
        public static CowVariant FromNbt(NbtTag nbt)
        {
            throw new NotImplementedException();
        }
    }
}