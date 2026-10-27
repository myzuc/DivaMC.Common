namespace DivaMC.Common.IO.Protocol.Packets.Configuration
{
    public sealed class SBEndResponsePacket : Packet
    {
        static SBEndResponsePacket()
        {
            Register<SBEndResponsePacket>(new(ProtocolDirection.Serverbound, ProtocolStage.Configuration,0x03), Deserialize);
        }
        
        public override void Serialize(Stream stream, PacketContext context)
        {
            base.Serialize(stream, context);
        }
        new private static Packet Deserialize(Stream stream, PacketContext context)
        {
            return new SBEndResponsePacket();
        }
    }
}