using System.Collections.Immutable;
using DivaMC.Common.Game.Objects.SpawnConditions;
using DivaMC.Common.Game.Registries;
using DivaMC.Common.IO.Serialization;
using DivaMC.Common.Primitives;
using fNbt;

namespace DivaMC.Common.Game.Variants.Entities
{
    public sealed record PigVariant : INbtSerializable<PigVariant>
    {
        public static readonly Identifier RegistryName = "minecraft:pig_variant";

        public static readonly PigVariant Stub = new()
        {
            AdultTexture = Identifier.Empty,
            BabyTexture = Identifier.Empty,
            Model = "normal",
            SpawnConditions = [],
        };
        public static readonly Registry<PigVariant> StubRegistry = new()
        {
            Id = RegistryName,
            Entries = [new(Identifier.Empty, Stub)]
        };
        
        public required Identifier AdultTexture { get; init; }
        public required Identifier BabyTexture { get; init; }
        public required string Model { get; init; }
        public required IImmutableList<SpawnCondition> SpawnConditions { get; init; }

        public PigVariant()
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
        public static PigVariant FromNbt(NbtTag nbt)
        {
            throw new NotImplementedException();
        }
    }
}