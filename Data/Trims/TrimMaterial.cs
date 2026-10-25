using DivaMC.Common.Chat.Components;
using DivaMC.Common.Common;
using DivaMC.Common.IO.Serialization;
using DivaMC.Common.Registries;
using fNbt;

namespace DivaMC.Common.Data.Trims
{
    public sealed record TrimMaterial : INbtSerializable<TrimMaterial>
    {
        public static readonly Identifier RegistryName = "minecraft:trim_material";

        public static readonly TrimMaterial Stub = new()
        {
            Name = string.Empty,
            PaletteId = "minecraft:trim_base"
        };
        public static readonly Registry<TrimMaterial> StubRegistry = new(RegistryName)
        {
            Entries = {
                ["minecraft:amethyst"] = Stub,
                ["minecraft:copper"] = Stub,
                ["minecraft:diamond"] = Stub,
                ["minecraft:emerald"] = Stub,
                ["minecraft:gold"] = Stub,
                ["minecraft:iron"] = Stub,
                ["minecraft:lapis"] = Stub,
                ["minecraft:netherite"] = Stub,
                ["minecraft:quartz"] = Stub,
                ["minecraft:redstone"] = Stub,
                ["minecraft:resin"] = Stub,
            }
        };
        public static readonly RegistryTags StubRegistryTags = new(RegistryName);
        
        public required ChatComponent Name { get; set; }
        public required Identifier PaletteId { get; set; }
        public TrimMaterial()
        {
            
        }
        public NbtTag ToNbt()
        {
            NbtCompound nbt = new();
            Name.ToNbt().Rename("description").AddTo(nbt);
            PaletteId.ToNbt().Rename("palette_id").AddTo(nbt);
            return nbt;
        }
        public static TrimMaterial FromNbt(NbtTag nbt)
        {
            throw new NotImplementedException();
        }
    }
}