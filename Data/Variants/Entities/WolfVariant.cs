using DivaMC.Common.Common;
using DivaMC.Common.Data.SpawnConditions;
using DivaMC.Common.IO.Serialization;
using DivaMC.Common.Registries;
using fNbt;

namespace DivaMC.Common.Data.Variants.Entities
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
        public static readonly Registry<WolfVariant> StubRegistry = new(RegistryName)
        {
            Entries =
            {
                [Identifier.Empty] = Stub
            }
        };
        public static readonly RegistryTags StubRegistryTags = new(RegistryName);
        
        public required WolfVariantAssets AdultTextures { get; set; }
        public required WolfVariantAssets BabyTextures { get; set; }
        public required List<SpawnCondition> SpawnConditions { get; set; }

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