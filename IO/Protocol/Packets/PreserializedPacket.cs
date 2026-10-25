using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Protocol.Packets
{
    public sealed record PreserializedPacket : Packet
    {
        public override PacketSignature Signature { get; }
        private byte[] Buffer { get; }
        public PreserializedPacket(Packet packet)
        {
            Signature = packet.Signature;
            using MemoryStream ms = new();
            Serialize(packet, ms);
            Buffer = ms.ToArray();
        }
        protected override void Serialize(Stream stream)
        {
            stream.WriteU8A(Buffer);
        }
    }
}