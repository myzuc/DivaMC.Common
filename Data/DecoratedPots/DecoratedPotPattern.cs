using DivaMC.Common.Chat.Components;
using DivaMC.Common.Common;
using DivaMC.Common.IO.Serialization;
using DivaMC.Common.Registries;
using fNbt;

namespace DivaMC.Common.Data.DecoratedPots
{
    public sealed record DecoratedPotPattern : INbtSerializable<DecoratedPotPattern>
    {
        public static readonly Identifier RegistryName = "minecraft:decorated_pot_pattern";

        public static readonly DecoratedPotPattern Stub = new()
        {
            Texture = Identifier.EmptyTexture
        };
        public static readonly Registry<DecoratedPotPattern> StubRegistry = new(RegistryName)
        {
            Entries = {
                ["minecraft:angler"] = Stub,
                ["minecraft:archer"] = Stub,
                ["minecraft:arms_up"] = Stub,
                ["minecraft:blade"] = Stub,
                ["minecraft:brewer"] = Stub,
                ["minecraft:burn"] = Stub,
                ["minecraft:danger"] = Stub,
                ["minecraft:explorer"] = Stub,
                ["minecraft:flow"] = Stub,
                ["minecraft:friend"] = Stub,
                ["minecraft:guster"] = Stub,
                ["minecraft:heart"] = Stub,
                ["minecraft:heartbreak"] = Stub,
                ["minecraft:howl"] = Stub,
                ["minecraft:miner"] = Stub,
                ["minecraft:mourner"] = Stub,
                ["minecraft:plenty"] = Stub,
                ["minecraft:prize"] = Stub,
                ["minecraft:scrape"] = Stub,
                ["minecraft:sheaf"] = Stub,
                ["minecraft:shelter"] = Stub,
                ["minecraft:skull"] = Stub,
                ["minecraft:snort"] = Stub,
            }
        };
        public static readonly RegistryTags StubRegistryTags = new(RegistryName);
        
        public required Identifier Texture { get; set; }
        public NbtTag ToNbt()
        {
            NbtCompound nbt = new();
            Texture.ToNbt().Rename("asset_id").AddTo(nbt);
            return nbt;
        }
        public static DecoratedPotPattern FromNbt(NbtTag nbt)
        {
            throw new NotImplementedException();
        }
    }
}