using DivaMC.Protocol.Common;
using DivaMC.Protocol.IO.Serialization;
using fNbt;

namespace DivaMC.Protocol.Data.Variants.Entities
{
    public sealed record WolfVariantAssets : INbtSerializable<WolfVariantAssets>
    {
        public static readonly WolfVariantAssets Stub = new()
        {
            Angry = Identifier.EmptyTexture,
            Wild = Identifier.EmptyTexture,
            Tame = Identifier.EmptyTexture,
        };
        
        public required Identifier Angry { get; set; }
        public required Identifier Wild { get; set; }
        public required Identifier Tame { get; set; }

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