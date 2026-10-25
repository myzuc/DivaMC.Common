using DivaMC.Common.Game.Chat.Components;
using DivaMC.Common.Game.Registries;
using DivaMC.Common.IO.Serialization;
using DivaMC.Common.Primitives;
using fNbt;

namespace DivaMC.Common.Game.Trims
{
    public sealed record TrimMaterial : INbtSerializable<TrimMaterial>
    {
        public static readonly Identifier RegistryName = "minecraft:trim_material";

        public static readonly TrimMaterial Stub = new()
        {
            Name = string.Empty,
            PaletteId = "minecraft:trim_base"
        };
        public static readonly Registry<TrimMaterial> StubRegistry = new()
        {
            Id = RegistryName,
            Entries = [
                new("minecraft:amethyst", Stub),
                new("minecraft:copper", Stub),
                new("minecraft:diamond", Stub),
                new("minecraft:emerald", Stub),
                new("minecraft:gold", Stub),
                new("minecraft:iron", Stub),
                new("minecraft:lapis", Stub),
                new("minecraft:netherite", Stub),
                new("minecraft:quartz", Stub),
                new("minecraft:redstone", Stub),
                new("minecraft:resin", Stub),
            ]
        };
        public static readonly RegistryTagList StubRegistryTags = new()
        {
            Id = RegistryName,
            Entries = []
        };
        
        public required ChatComponent Name { get; init; }
        public required Identifier PaletteId { get; init; }
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