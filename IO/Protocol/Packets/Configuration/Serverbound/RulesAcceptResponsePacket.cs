using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Protocol.Packets.Configuration.Serverbound
{
    public sealed record RulesAcceptResponsePacket : Packet
    {
        static RulesAcceptResponsePacket()
        {
            PacketSignature signature = new PacketSignature(
                ProtocolDirection.Serverbound,
                ProtocolStage.Configuration,
                0x09
            );
            Packet.Register<RulesAcceptResponsePacket>(signature, Deserialize);
        }
        
        protected override void Serialize(Stream stream)
        {
            
        }
        private static Packet Deserialize(Stream stream)
        {
            return new RulesAcceptResponsePacket();
        }
    }
}