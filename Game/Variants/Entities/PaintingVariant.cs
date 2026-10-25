using DivaMC.Common.Game.Chat.Components;
using DivaMC.Common.Game.Registries;
using DivaMC.Common.IO.Serialization;
using DivaMC.Common.Primitives;
using fNbt;

namespace DivaMC.Common.Game.Variants.Entities
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
        public static readonly Registry<PaintingVariant> StubRegistry = new()
        {
            Id = RegistryName,
            Entries = [new(Identifier.Empty, Stub)]
        };
        
        public required Identifier Texture { get; init; }
        public required int Width { get; init; }
        public required int Height { get; init; }
        public required ChatComponent Title { get; init; }
        public required ChatComponent Author { get; init; }

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