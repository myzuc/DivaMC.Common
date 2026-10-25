using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Protocol.Packets.Configuration.Serverbound
{
    public sealed record RulesAcceptResponse : Packet
    {
        static RulesAcceptResponse()
        {
            PacketSignature signature = new PacketSignature(
                ProtocolDirection.Serverbound,
                ProtocolStage.Configuration,
                0x09
            );
            Packet.Register<RulesAcceptResponse>(signature, Deserialize);
        }
        
        protected override void Serialize(Stream stream, Context context)
        {
            
        }
        private static Packet Deserialize(Stream stream, Context context)
        {
            return new RulesAcceptResponse();
        }
    }
}