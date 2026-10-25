using System.Collections.Immutable;
using DivaMC.Common.Game.Objects.SpawnConditions;
using DivaMC.Common.Game.Registries;
using DivaMC.Common.IO.Serialization;
using DivaMC.Common.Primitives;
using fNbt;

namespace DivaMC.Common.Game.Variants.Entities
{
    public sealed record FrogVariant : INbtSerializable<FrogVariant>
    {
        public static readonly Identifier RegistryName = "minecraft:frog_variant";

        public static readonly FrogVariant Stub = new()
        {
            Texture = Identifier.EmptyTexture,
            SpawnConditions = [],
        };
        public static readonly Registry<FrogVariant> StubRegistry = new()
        {
            Id = RegistryName,
            Entries = [new(Identifier.Empty, Stub)]
        };
        
        public required Identifier Texture { get; init; }
        public required IImmutableList<SpawnCondition> SpawnConditions { get; init; }

        public FrogVariant()
        {
            
        }
        
        public NbtTag ToNbt()
        {
            NbtCompound nbt = new();
            Texture.ToNbt().Rename("asset_id").AddTo(nbt);
            NbtList.FromIEnumerable(SpawnConditions).Rename("spawn_conditions").AddTo(nbt);
            return nbt;
        }
        public static FrogVariant FromNbt(NbtTag nbt)
        {
            throw new NotImplementedException();
        }
    }
}