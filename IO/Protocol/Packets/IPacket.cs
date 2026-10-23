namespace Net.Myzuc.DivaMC.Common.IO.Protocol.Packets
{
    public interface IPacket
    {
        public static abstract PacketSignature Signature { get; }
        public void Serialize(Stream stream, object registry);
        static abstract IPacket Deserialize(Stream stream, object registry);
    }
}