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
                ["minecraft:pattern_item/bordure_indented"] = Stub,
                ["minecraft:pattern_item/creeper"] = Stub,
                ["minecraft:pattern_item/field_masoned"] = Stub,
                ["minecraft:pattern_item/flow"] = Stub,
                ["minecraft:pattern_item/flower"] = Stub,
                ["minecraft:pattern_item/globe"] = Stub,
                ["minecraft:pattern_item/guster"] = Stub,
                ["minecraft:pattern_item/mojang"] = Stub,
                ["minecraft:pattern_item/piglin"] = Stub,
                ["minecraft:pattern_item/skull"] = Stub,
            }
        };
        public static readonly RegistryTags StubRegistryTags = new(RegistryName)
        {
            Entries =
            {
                ["minecraft:pattern_item/bordure_indented"] =
                [
                    StubRegistry.IdOf("minecraft:pattern_item/bordure_indented")
                ],
                ["minecraft:pattern_item/creeper"] =
                [
                    StubRegistry.IdOf("minecraft:pattern_item/creeper")
                ],
                ["minecraft:pattern_item/field_masoned"] =
                [
                    StubRegistry.IdOf("minecraft:pattern_item/field_masoned")
                ],
                ["minecraft:pattern_item/flow"] =
                [
                    StubRegistry.IdOf("minecraft:pattern_item/flow")
                ],
                ["minecraft:pattern_item/flower"] =
                [
                    StubRegistry.IdOf("minecraft:pattern_item/flower")
                ],
                ["minecraft:pattern_item/globe"] =
                [
                    StubRegistry.IdOf("minecraft:pattern_item/globe")
                ],
                ["minecraft:pattern_item/guster"] =
                [
                    StubRegistry.IdOf("minecraft:pattern_item/guster")
                ],
                ["minecraft:pattern_item/mojang"] =
                [
                    StubRegistry.IdOf("minecraft:pattern_item/mojang")
                ],
                ["minecraft:pattern_item/piglin"] =
                [
                    StubRegistry.IdOf("minecraft:pattern_item/piglin")
                ],
                ["minecraft:pattern_item/skull"] =
                [
                    StubRegistry.IdOf("minecraft:pattern_item/skull")
                ],
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