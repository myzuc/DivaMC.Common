using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Protocol.Packets
{
    public sealed class PreserializedPacket : Packet
    {
        public override PacketSignature Signature { get; }
        private PacketContext Context { get; }
        private byte[] Buffer { get; }
        public PreserializedPacket(Packet packet, PacketContext context)
        {
            Signature = packet.Signature;
            Context = context;
            using MemoryStream ms = new();
            packet.Serialize(ms, context);
            Buffer = ms.ToArray();
        }
        public override void Serialize(Stream stream, PacketContext context)
        {
            base.Serialize(stream, context);
            context.Check(Signature);
            stream.WriteU8A(Buffer);
        }
    }
}