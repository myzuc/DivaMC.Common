using DivaMC.Common.Common;
using DivaMC.Common.Data.SpawnConditions;
using DivaMC.Common.IO.Serialization;
using DivaMC.Common.Registries;
using fNbt;

namespace DivaMC.Common.Data.Variants.Entities
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
        public static readonly Registry<CatVariant> StubRegistry = new(RegistryName)
        {
            Entries =
            {
                [Identifier.Empty] = Stub
            }
        };
        public static readonly RegistryTags StubRegistryTags = new(RegistryName);
        
        public required Identifier AdultTexture { get; set; }
        public required Identifier BabyTexture { get; set; }
        public required List<SpawnCondition> SpawnConditions { get; set; }

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