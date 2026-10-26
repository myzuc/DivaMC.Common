using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Protocol.Packets.Configuration.Clientbound
{
    public sealed record EndPacket : Packet
    {
        static EndPacket()
        {
            PacketSignature signature = new PacketSignature(
                ProtocolDirection.Clientbound,
                ProtocolStage.Configuration,
                0x03
            );
            Packet.Register<EndPacket>(signature, Deserialize);
        }
        
        protected override void Serialize(Stream stream)
        {
        }
        private static Packet Deserialize(Stream stream)
        {
            return new EndPacket();
        }
    }
}