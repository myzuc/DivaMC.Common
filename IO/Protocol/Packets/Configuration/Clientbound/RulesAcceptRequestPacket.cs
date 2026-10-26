using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Protocol.Packets.Configuration.Clientbound
{
    public sealed record RulesAcceptRequestPacket : Packet
    {
        static RulesAcceptRequestPacket()
        {
            PacketSignature signature = new PacketSignature(
                ProtocolDirection.Clientbound,
                ProtocolStage.Configuration,
                0x14
            );
            Packet.Register<RulesAcceptRequestPacket>(signature, Deserialize);
        }
        
        public required string Message { get; set; }
        
        protected override void Serialize(Stream stream)
        {
            stream.WriteT0AVS32(Message);
        }
        private static Packet Deserialize(Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}