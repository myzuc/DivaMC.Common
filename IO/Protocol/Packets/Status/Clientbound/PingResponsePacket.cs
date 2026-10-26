using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Protocol.Packets.Status.Clientbound
{
    public sealed record PingResponsePacket : Packet
    {
        static PingResponsePacket()
        {
            PacketSignature signature = new PacketSignature(
                ProtocolDirection.Clientbound,
                ProtocolStage.Status,
                0x01
            );
            Packet.Register<PingResponsePacket>(signature, Deserialize);
        }
        
        public required long Data { get; set; }
        
        protected override void Serialize(Stream stream)
        {
            stream.WriteS64(Data);
        }
        private static Packet Deserialize(Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}