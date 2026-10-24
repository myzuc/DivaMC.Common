namespace Net.Myzuc.DivaMC.Common.IO.Protocol.Packets.Status.Serverbound
{
    public sealed record StatusRequest : Packet
    {
        static StatusRequest()
        {
            PacketSignature signature = new PacketSignature(
                ProtocolDirection.Serverbound,
                ProtocolStage.Status,
                0x00
            );
            Packet.Register<StatusRequest>(signature, Deserialize);
        }
        
        protected override void Serialize(Stream stream, object registry)
        {
            throw new NotImplementedException();
        }
        private static Packet Deserialize(Stream stream, object registry)
        {
            return new StatusRequest();
        }
    }
}