namespace DivaMC.Common.IO.Protocol.Packets.Status
{
    public sealed class SBStatusRequestPacket : Packet
    {
        static SBStatusRequestPacket()
        {
            Register<SBStatusRequestPacket>(new(ProtocolDirection.Serverbound, ProtocolStage.Status,0x00), Deserialize);
        }
        
        public override void Serialize(Stream stream, PacketContext context)
        {
            throw new NotImplementedException();
        }
        new private static Packet Deserialize(Stream stream, PacketContext context)
        {
            return new SBStatusRequestPacket();
        }
    }
}