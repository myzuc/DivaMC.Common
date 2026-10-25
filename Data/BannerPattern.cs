using DivaMC.Common.Chat.Components;
using DivaMC.Common.Common;
using DivaMC.Common.IO.Serialization;
using DivaMC.Common.Registries;
using fNbt;

namespace DivaMC.Common.Data
{
    public sealed record BannerPattern : INbtSerializable<BannerPattern>
    {
        public static readonly Identifier RegistryName = "minecraft:banner_pattern";

        public static readonly BannerPattern Stub = new()
        {
            Texture = Identifier.EmptyTexture,
            TranslationKey = string.Empty
        };
        public static readonly Registry<BannerPattern> StubRegistry = new(RegistryName)
        {
            Entries = {
                [Identifier.Empty] = Stub,
            }
        };
        public static readonly RegistryTags StubRegistryTags = new(RegistryName)
        {
            Entries =
            {
                ["minecraft:pattern_item/bordure_indented"] = [0],
                ["minecraft:pattern_item/creeper"] = [0],
                ["minecraft:pattern_item/field_masoned"] = [0],
                ["minecraft:pattern_item/flow"] = [0],
                ["minecraft:pattern_item/flower"] = [0],
                ["minecraft:pattern_item/globe"] = [0],
                ["minecraft:pattern_item/guster"] = [0],
                ["minecraft:pattern_item/mojang"] = [0],
                ["minecraft:pattern_item/piglin"] = [0],
                ["minecraft:pattern_item/skull"] = [0],
            }
        };
        
        public required Identifier Texture { get; set; }
        public required string TranslationKey { get; set; }
        public NbtTag ToNbt()
        {
            NbtCompound nbt = new();
            Texture.ToNbt().Rename("asset_id").AddTo(nbt);
            NbtString.FromT0A(TranslationKey).Rename("translation_key").AddTo(nbt);
            return nbt;
        }
        public static BannerPattern FromNbt(NbtTag nbt)
        {
            throw new NotImplementedException();
        }
    }
}