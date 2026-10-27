using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Protocol.Packets
{
    public sealed class UnknownPacket : Packet
    {
        public override PacketSignature Signature { get; }
        public byte[] Buffer { get; set; }
        public UnknownPacket(PacketSignature signature, byte[] buffer)
        {
            Signature = signature;
            Buffer = buffer;
        }
        public override void Serialize(Stream stream, PacketContext context)
        {
            base.Serialize(stream, context);
            context.Check(Signature);
            context.Check(Signature);
            stream.WriteU8A(Buffer);
        }
    }
}