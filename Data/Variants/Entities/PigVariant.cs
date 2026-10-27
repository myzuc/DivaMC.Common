using DivaMC.Protocol.Common;
using DivaMC.Protocol.Data.SpawnConditions;
using DivaMC.Protocol.IO.Serialization;
using DivaMC.Protocol.Registries;
using fNbt;

namespace DivaMC.Protocol.Data.Variants.Entities
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
        public static readonly Registry<PigVariant> StubRegistry = new(RegistryName)
        {
            Entries =
            {
                [Identifier.Empty] = Stub
            }
        };
        public static readonly RegistryTags StubRegistryTags = new(RegistryName);
        
        public required Identifier AdultTexture { get; set; }
        public required Identifier BabyTexture { get; set; }
        public required string Model { get; set; }
        public required List<SpawnCondition> SpawnConditions { get; set; }

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