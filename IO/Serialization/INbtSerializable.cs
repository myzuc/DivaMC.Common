using fNbt;

namespace DivaMC.Protocol.IO.Serialization
{
    public interface INbtSerializable<T>
    {
        public NbtTag ToNbt();
        public static abstract T FromNbt(NbtTag nbt);
    }
}