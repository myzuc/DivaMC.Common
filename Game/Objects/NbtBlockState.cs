using DivaMC.Common.IO.Serialization;
using DivaMC.Common.Primitives;
using fNbt;

namespace DivaMC.Common.Game.Objects
{
    public sealed record NbtBlockState : INbtSerializable<NbtBlockState>
    {
        public Identifier Id;
        public NbtCompound Properties;
        public NbtBlockState(Identifier id)
        {
            Id = id;
            Properties = new();
        }
        public NbtBlockState(Identifier id, NbtCompound properties)
        {
            Id = id;
            Properties = properties;
        }
        
        public NbtTag ToNbt()
        {
            NbtCompound nbt = new();
            Id.ToNbt().Rename("id").AddTo(nbt);
            Properties.Rename("properties").AddTo(nbt);
            return nbt;
        }
        public static NbtBlockState FromNbt(NbtTag nbt)
        {
            throw new NotImplementedException();
        }
        
        public static implicit operator NbtBlockState(Identifier id)
        {
            return new(id);
        }
        public static implicit operator NbtBlockState(string id)
        {
            return new(id);
        }
    }
}