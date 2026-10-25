namespace DivaMC.Common.IO.Serialization
{
    internal interface IBinarySerializable<T>
    {
        internal void Serialize(Stream stream);
        internal static abstract T Deserialize(Stream stream);
    }
}