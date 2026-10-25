using DivaMC.Common.Game.Objects;
using DivaMC.Common.IO.Serialization;
using DivaMC.Common.Primitives;
using fNbt;

namespace DivaMC.Common.Game.Variants.Entities
{
    public sealed record WolfVariantAssets : INbtSerializable<WolfVariantAssets>
    {
        public static readonly WolfVariantAssets Stub = new()
        {
            Angry = Identifier.EmptyTexture,
            Wild = Identifier.EmptyTexture,
            Tame = Identifier.EmptyTexture,
        };
        
        public required Identifier Angry { get; init; }
        public required Identifier Wild { get; init; }
        public required Identifier Tame { get; init; }

        public WolfVariantAssets()
        {
            
        }
        
        public NbtTag ToNbt()
        {
            NbtCompound nbt = new();
            Angry.ToNbt().Rename("angry").AddTo(nbt);
            Wild.ToNbt().Rename("wild").AddTo(nbt);
            Tame.ToNbt().Rename("tame").AddTo(nbt);
            return nbt;
        }
        public static WolfVariantAssets FromNbt(NbtTag nbt)
        {
            throw new NotImplementedException();
        }
    }
}