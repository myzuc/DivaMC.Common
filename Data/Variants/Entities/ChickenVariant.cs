using DivaMC.Common.Common;
using DivaMC.Common.Data.SpawnConditions;
using DivaMC.Common.IO.Serialization;
using DivaMC.Common.Registries;
using fNbt;

namespace DivaMC.Common.Data.Variants.Entities
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
        public static readonly Registry<ChickenVariant> StubRegistry = new(RegistryName)
        {
            Entries = {
                ["minecraft:warm"] = Stub,
                ["minecraft:temperate"] = Stub,
                ["minecraft:cold"] = Stub
            }
        };
        public static readonly RegistryTags StubRegistryTags = new(RegistryName);
        
        public required Identifier AdultTexture { get; set; }
        public required Identifier BabyTexture { get; set; }
        public required string Model { get; set; }
        public required List<SpawnCondition> SpawnConditions { get; set; }

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