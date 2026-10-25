using DivaMC.Common.Chat.Components;
using DivaMC.Common.Common;
using DivaMC.Common.IO.Serialization;
using DivaMC.Common.Registries;
using fNbt;

namespace DivaMC.Common.Data.Trims
{
    public sealed record TrimPattern : INbtSerializable<TrimPattern>
    {
        public static readonly Identifier RegistryName = "minecraft:trim_pattern";

        public static readonly TrimPattern Stub = new()
        {
            Name = string.Empty,
            AssetId = Identifier.Empty,
            BlendAlpha = false,
        };
        public static readonly Registry<TrimPattern> StubRegistry = new(RegistryName)
        {
            Entries = []
        };
        public static readonly RegistryTags StubRegistryTags = new(RegistryName);
        
        public required ChatComponent Name { get; set; }
        public required Identifier AssetId { get; set; }
        public required bool BlendAlpha { get; set; }
        public TrimPattern()
        {
            
        }
        public NbtTag ToNbt()
        {
            NbtCompound nbt = new();
            Name.ToNbt().Rename("description").AddTo(nbt);
            AssetId.ToNbt().Rename("asset_id").AddTo(nbt);
            NbtByte.FromU1(BlendAlpha).Rename("decal").AddTo(nbt);
            return nbt;
        }
        public static TrimPattern FromNbt(NbtTag nbt)
        {
            throw new NotImplementedException();
        }
    }
}