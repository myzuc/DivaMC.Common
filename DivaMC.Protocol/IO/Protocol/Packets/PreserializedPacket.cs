using DivaMC.Protocol.IO.Serialization;

namespace DivaMC.Protocol.IO.Protocol.Packets
{
    public sealed class PreserializedPacket : Packet
    {
        private PacketContext Context { get; }
        private byte[] Buffer { get; }
        public PreserializedPacket(Packet packet, PacketContext context)
        {
            Context = context;
            using MemoryStream ms = new();
            packet.Serialize(ms, context);
            Buffer = ms.ToArray();
        }
        public override void Serialize(Stream stream, PacketContext context)
        {
            if (Context != context) throw new InvalidOperationException();
            base.Serialize(stream, context);
            stream.WriteU8A(Buffer);
        }
    }
}