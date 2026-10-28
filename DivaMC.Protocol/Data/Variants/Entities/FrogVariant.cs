using DivaMC.Protocol.Common;
using DivaMC.Protocol.Data.SpawnConditions;
using DivaMC.Protocol.IO.Serialization;
using DivaMC.Protocol.Registries;
using fNbt;

namespace DivaMC.Protocol.Data.Variants.Entities
{
    public sealed record FrogVariant : INbtSerializable<FrogVariant>
    {
        public static readonly Identifier RegistryName = "minecraft:frog_variant";

        public static readonly FrogVariant Stub = new()
        {
            Texture = Identifier.EmptyTexture,
            SpawnConditions = [],
        };
        public static readonly Registry<FrogVariant> StubRegistry = new(RegistryName)
        {
            Entries =
            {
                [Identifier.Empty] = Stub
            }
        };
        public static readonly RegistryTags StubRegistryTags = new(RegistryName);
        
        public required Identifier Texture { get; set; }
        public required List<SpawnCondition> SpawnConditions { get; set; }

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