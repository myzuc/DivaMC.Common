using System.Collections.Immutable;
using DivaMC.Common.Game.Objects.SpawnConditions;
using DivaMC.Common.Game.Registries;
using DivaMC.Common.IO.Serialization;
using DivaMC.Common.Primitives;
using fNbt;

namespace DivaMC.Common.Game.Variants.Entities
{
    public sealed record ChickenVariant : INbtSerializable<ChickenVariant>
    {
        public static readonly Identifier RegistryName = "minecraft:chicken_variant";

        public static readonly ChickenVariant Stub = new()
        {
            AdultTexture = Identifier.Empty,
            BabyTexture = Identifier.Empty,
            Model = "normal",
            SpawnConditions = [],
        };
        public static readonly Registry<ChickenVariant> StubRegistry = new()
        {
            Id = RegistryName,
            Entries = [
                new("minecraft:warm", Stub),
                new("minecraft:temperate", Stub),
                new("minecraft:cold", Stub)
            ]
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

        public ChickenVariant()
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
        public static ChickenVariant FromNbt(NbtTag nbt)
        {
            throw new NotImplementedException();
        }
    }
}