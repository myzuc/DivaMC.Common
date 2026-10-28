using DivaMC.Protocol.IO.Serialization;

namespace DivaMC.Protocol.IO.Protocol.Packets
{
    public sealed class UnknownPacket : Packet
    {
        public int Id { get; set; }
        public byte[] Buffer { get; set; }
        public UnknownPacket(int id, byte[] buffer)
        {
            Id = id;
            Buffer = buffer;
        }
        public override void Serialize(Stream stream, PacketContext context)
        {
            base.Serialize(stream, context);
            stream.WriteU8A(Buffer);
        }
    }
}