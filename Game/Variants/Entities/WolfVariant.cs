using System.Collections.Immutable;
using DivaMC.Common.Game.Objects.SpawnConditions;
using DivaMC.Common.Game.Registries;
using DivaMC.Common.IO.Serialization;
using DivaMC.Common.Primitives;
using fNbt;

namespace DivaMC.Common.Game.Variants.Entities
{
    public sealed record WolfVariant : INbtSerializable<WolfVariant>
    {
        public static readonly Identifier RegistryName = "minecraft:wolf_variant";

        public static readonly WolfVariant Stub = new()
        {
            AdultTextures = WolfVariantAssets.Stub,
            BabyTextures = WolfVariantAssets.Stub,
            SpawnConditions = [],
        };
        public static readonly Registry<WolfVariant> StubRegistry = new()
        {
            Id = RegistryName,
            Entries = [new(Identifier.Empty, Stub)]
        };
        
        public required WolfVariantAssets AdultTextures { get; init; }
        public required WolfVariantAssets BabyTextures { get; init; }
        public required IImmutableList<SpawnCondition> SpawnConditions { get; init; }

        public WolfVariant()
        {
            
        }
        
        public NbtTag ToNbt()
        {
            NbtCompound nbt = new();
            AdultTextures.ToNbt().Rename("assets").AddTo(nbt);
            BabyTextures.ToNbt().Rename("baby_assets").AddTo(nbt);
            NbtList.FromIEnumerable(SpawnConditions).Rename("spawn_conditions").AddTo(nbt);
            return nbt;
        }
        public static WolfVariant FromNbt(NbtTag nbt)
        {
            throw new NotImplementedException();
        }
    }
}