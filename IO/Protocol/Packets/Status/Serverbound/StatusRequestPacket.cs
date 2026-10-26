namespace DivaMC.Common.IO.Protocol.Packets.Status.Serverbound
{
    public sealed record StatusRequestPacket : Packet
    {
        static StatusRequestPacket()
        {
            PacketSignature signature = new PacketSignature(
                ProtocolDirection.Serverbound,
                ProtocolStage.Status,
                0x00
            );
            Packet.Register<StatusRequestPacket>(signature, Deserialize);
        }
        
        protected override void Serialize(Stream stream)
        {
            throw new NotImplementedException();
        }
        private static Packet Deserialize(Stream stream)
        {
            return new StatusRequestPacket();
        }
    }
}