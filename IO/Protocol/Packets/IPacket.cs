using Me.Shiokawaii.IO;

namespace Net.Myzuc.DivaMC.Common.IO.Protocol.Packets
{
    public interface IPacket
    {
        public static abstract PacketSignature Signature { get; }
        public void Serialize(BinaryStream stream, object registry);
        static abstract IPacket Deserialize(BinaryStream stream, object registry);
    }
}