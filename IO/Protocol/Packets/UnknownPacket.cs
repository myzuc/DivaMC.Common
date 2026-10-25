using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Protocol.Packets
{
    public sealed record UnknownPacket : Packet
    {
        public override PacketSignature Signature { get; }
        public byte[] Buffer { get; set; }
        public UnknownPacket(PacketSignature signature, byte[] buffer)
        {
            Signature = signature;
            Buffer = buffer;
        }
        protected override void Serialize(Stream stream)
        {
            stream.WriteU8A(Buffer);
        }
    }
}