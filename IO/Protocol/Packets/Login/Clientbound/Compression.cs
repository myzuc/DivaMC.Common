using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Protocol.Packets.Login.Clientbound
{
    public sealed record Compression : Packet
    {
        static Compression()
        {
            PacketSignature signature = new PacketSignature(
                ProtocolDirection.Clientbound,
                ProtocolStage.Login,
                0x03
            );
            Packet.Register<Compression>(signature, Deserialize);
        }
        
        public required int Threshold { get; init; }
        
        protected override void Serialize(Stream stream, object registry)
        {
            stream.WriteVS32(Threshold);
        }
        private static Packet Deserialize(Stream stream, object registry)
        {
            throw new NotImplementedException();
        }
    }
}