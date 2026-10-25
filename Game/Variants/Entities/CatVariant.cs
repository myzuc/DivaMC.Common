using System.Collections.Immutable;
using DivaMC.Common.Game.Objects.SpawnConditions;
using DivaMC.Common.Game.Registries;
using DivaMC.Common.IO.Serialization;
using DivaMC.Common.Primitives;
using fNbt;

namespace DivaMC.Common.Game.Variants.Entities
{
    public sealed record CatVariant : INbtSerializable<CatVariant>
    {
        public static readonly Identifier RegistryName = "minecraft:cat_variant";

        public static readonly CatVariant Stub = new()
        {
            AdultTexture = Identifier.Empty,
            BabyTexture = Identifier.Empty,
            SpawnConditions = [],
        };
        public static readonly Registry<CatVariant> StubRegistry = new()
        {
            Id = RegistryName,
            Entries = [new(Identifier.Empty, Stub)]
        };
        
        public required Identifier AdultTexture { get; init; }
        public required Identifier BabyTexture { get; init; }
        public required IImmutableList<SpawnCondition> SpawnConditions { get; init; }

        public CatVariant()
        {
            
        }
        
        public NbtTag ToNbt()
        {
            NbtCompound nbt = new();
            AdultTexture.ToNbt().Rename("asset_id").AddTo(nbt);
            BabyTexture.ToNbt().Rename("baby_asset_id").AddTo(nbt);
            NbtList.FromIEnumerable(SpawnConditions).Rename("spawn_conditions").AddTo(nbt);
            return nbt;
        }
        public static CatVariant FromNbt(NbtTag nbt)
        {
            throw new NotImplementedException();
        }
    }
}