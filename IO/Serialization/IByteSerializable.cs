namespace DivaMC.Protocol.IO.Serialization
{
    internal interface IByteSerializable<T>
    {
        internal void Serialize(Stream stream);
        internal static abstract T Deserialize(Stream stream);
    }
    internal interface IByteSerializable<T, TContext>
    {
        internal void Serialize(Stream stream, TContext context);
        internal static abstract T Deserialize(Stream stream, TContext context);
    }
}