namespace DivaMC.Common.IO.Serialization
{
    internal interface IBinarySerializable<T>
    {
        internal void Serialize(Stream stream);
        internal static abstract T Deserialize(Stream stream);
    }
    internal interface IBinarySerializable<T, TContext>
    {
        internal void Serialize(Stream stream, TContext context);
        internal static abstract T Deserialize(Stream stream, TContext context);
    }
}