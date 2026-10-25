using DivaMC.Common.Game.Chat.Components;
using DivaMC.Common.Game.Registries;
using DivaMC.Common.IO.Serialization;
using DivaMC.Common.Primitives;
using fNbt;

namespace DivaMC.Common.Game.Trims
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
        public static readonly Registry<TrimPattern> StubRegistry = new()
        {
            Id = RegistryName,
            Entries = []
        };
        public static readonly RegistryTagList StubRegistryTags = new()
        {
            Id = RegistryName,
            Entries = []
        };
        
        public required ChatComponent Name { get; init; }
        public required Identifier AssetId { get; init; }
        public required bool BlendAlpha { get; init; }
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