using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Protocol.Packets.Configuration
{
    public sealed class CBRulesAcceptRequestPacket : Packet
    {
        static CBRulesAcceptRequestPacket()
        {
            Register<CBRulesAcceptRequestPacket>(new(ProtocolDirection.Clientbound, ProtocolStage.Configuration,0x14), Deserialize);
        }
        
        public required string Message { get; set; }
        
        public override void Serialize(Stream stream, PacketContext context)
        {
            base.Serialize(stream, context);
            stream.WriteT0AVS32(Message);
        }
        new private static Packet Deserialize(Stream stream, PacketContext context)
        {
            throw new NotImplementedException();
        }
    }
}