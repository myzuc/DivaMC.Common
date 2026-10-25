using fNbt;

namespace DivaMC.Common.IO.Serialization
{
    public interface INbtSerializable<T>
    {
        public NbtTag ToNbt();
        public static abstract T FromNbt(NbtTag nbt);
    }
}