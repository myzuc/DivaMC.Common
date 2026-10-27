namespace DivaMC.Common.IO.Protocol.Packets.Configuration
{
    public sealed class SBRulesAcceptResponsePacket : Packet
    {
        static SBRulesAcceptResponsePacket()
        {
            Register<SBRulesAcceptResponsePacket>(new(ProtocolDirection.Serverbound, ProtocolStage.Configuration,0x09), Deserialize);
        }
        
        public override void Serialize(Stream stream, PacketContext context)
        {
            base.Serialize(stream, context);
        }
        new private static Packet Deserialize(Stream stream, PacketContext context)
        {
            return new SBRulesAcceptResponsePacket();
        }
    }
}