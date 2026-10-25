using DivaMC.Common.Chat.Components;
using DivaMC.Common.Common;
using DivaMC.Common.IO.Serialization;
using DivaMC.Common.Registries;
using fNbt;

namespace DivaMC.Common.Data.Variants.Entities
{
    public sealed record PaintingVariant : INbtSerializable<PaintingVariant>
    {
        public static readonly Identifier RegistryName = "minecraft:painting_variant";

        public static readonly PaintingVariant Stub = new()
        {
            Texture = Identifier.Empty,
            Width = 1,
            Height = 1,
            Title = string.Empty,
            Author = string.Empty,
        };
        public static readonly Registry<PaintingVariant> StubRegistry = new(RegistryName)
        {
            Entries =
            {
                [Identifier.Empty] = Stub
            }
        };
        public static readonly RegistryTags StubRegistryTags = new(RegistryName);
        
        public required Identifier Texture { get; set; }
        public required int Width { get; set; }
        public required int Height { get; set; }
        public required ChatComponent Title { get; set; }
        public required ChatComponent Author { get; set; }

        public PaintingVariant()
        {
            
        }
        
        public NbtTag ToNbt()
        {
            NbtCompound nbt = new();
            Texture.ToNbt().Rename("asset_id").AddTo(nbt);
            NbtInt.FromS32(Width).Rename("width").AddTo(nbt);
            NbtInt.FromS32(Height).Rename("height").AddTo(nbt);
            Title.ToNbt().Rename("title").AddTo(nbt);
            Author.ToNbt().Rename("author").AddTo(nbt);
            return nbt;
        }
        public static PaintingVariant FromNbt(NbtTag nbt)
        {
            throw new NotImplementedException();
        }
    }
}