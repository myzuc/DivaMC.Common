using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Protocol.Packets.Configuration.Clientbound
{
    public sealed record PingRequestPacket : Packet
    {
        static PingRequestPacket()
        {
            PacketSignature signature = new PacketSignature(
                ProtocolDirection.Clientbound,
                ProtocolStage.Configuration,
                0x05
            );
            Packet.Register<PingRequestPacket>(signature, Deserialize);
        }
        
        public required int Id { get; set; }
        
        protected override void Serialize(Stream stream)
        {
            stream.WriteS32(Id);
        }
        private static Packet Deserialize(Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}