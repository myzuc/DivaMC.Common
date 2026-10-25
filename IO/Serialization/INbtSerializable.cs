using fNbt;

namespace DivaMC.Common.IO.Serialization
{
    internal interface INbtSerializable<T>
    {
        internal NbtTag ToNbt();
        internal static abstract T FromNbt(NbtTag nbt);
    }
}