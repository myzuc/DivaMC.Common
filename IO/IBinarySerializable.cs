namespace Net.Myzuc.DivaMC.Common.IO
{
    public interface IBinarySerializable<T>
    {
        internal abstract void Serialize(Stream stream, T data);
        internal static abstract T Deserialize(Stream stream);
    }
}