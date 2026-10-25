using DivaMC.Common.Common;
using DivaMC.Common.Data.SpawnConditions;
using DivaMC.Common.IO.Serialization;
using DivaMC.Common.Registries;
using fNbt;

namespace DivaMC.Common.Data.Variants.Entities
{
    public sealed record ZombieNautilusVariant : INbtSerializable<ZombieNautilusVariant>
    {
        public static readonly Identifier RegistryName = "minecraft:zombie_nautilus_variant";

        public static readonly ZombieNautilusVariant Stub = new()
        {
            Texture = Identifier.Empty,
            Model = "normal",
            SpawnConditions = [],
        };
        public static readonly Registry<ZombieNautilusVariant> StubRegistry = new(RegistryName)
        {
            Entries =
            {
                [Identifier.Empty] = Stub
            }
        };
        public static readonly RegistryTags StubRegistryTags = new(RegistryName);
        
        public required Identifier Texture { get; set; }
        public required string Model { get; set; }
        public required List<SpawnCondition> SpawnConditions { get; set; }

        public ZombieNautilusVariant()
        {
            
        }
        
        public NbtTag ToNbt()
        {
            NbtCompound nbt = new();
            Texture.ToNbt().Rename("asset_id").AddTo(nbt);
            NbtString.FromT0A(Model).Rename("model").AddTo(nbt);
            NbtList.FromIEnumerable(SpawnConditions).Rename("spawn_conditions").AddTo(nbt);
            return nbt;
        }
        public static ZombieNautilusVariant FromNbt(NbtTag nbt)
        {
            throw new NotImplementedException();
        }
    }
}