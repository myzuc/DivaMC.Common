namespace DivaMC.Common.IO
{
    internal interface IBinarySerializable<T>
    {
        internal void Serialize(Stream stream);
        internal static abstract T Deserialize(Stream stream);
    }
}