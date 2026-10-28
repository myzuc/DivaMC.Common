namespace DivaMC.Protocol.IO.Protocol.Packets.Configuration
{
    public sealed class CBEndRequestPacket : Packet
    {
        static CBEndRequestPacket()
        {
            Register<CBEndRequestPacket>(new(ProtocolDirection.Clientbound, ProtocolStage.Configuration,0x03), Deserialize);
        }
        
        public override void Serialize(Stream stream, PacketContext context)
        {
            base.Serialize(stream, context);
        }
        new private static Packet Deserialize(Stream stream, PacketContext context)
        {
            return new CBEndRequestPacket();
        }
    }
}