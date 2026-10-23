namespace Net.Myzuc.DivaMC.Common.IO.Protocol.Packets.Handshake.Serverbound
{
    public sealed record StartPacket : IPacket
    {
        static StartPacket()
        {
            Packet.Register<StartPacket>();
        }
        public static PacketSignature Signature => 
            new PacketSignature(
                ProtocolDirection.Serverbound,
                ProtocolStage.Handshake,
                0x00
            );
        
        public void Serialize(Stream stream, object registry)
        {
            throw new NotImplementedException();
        }
        public static IPacket Deserialize(Stream stream, object registry)
        {
            throw new NotImplementedException();
        }
    }
}