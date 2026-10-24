namespace DivaMC.Common.IO
{
    public interface IBinarySerializable<T>
    {
        internal void Serialize(Stream stream);
        internal static abstract T Deserialize(Stream stream);
    }
}