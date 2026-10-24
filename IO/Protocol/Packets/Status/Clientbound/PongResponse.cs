namespace Net.Myzuc.DivaMC.Common.IO.Protocol.Packets.Status.Clientbound
{
    public sealed record PongResponse : Packet
    {
        static PongResponse()
        {
            PacketSignature signature = new PacketSignature(
                ProtocolDirection.Clientbound,
                ProtocolStage.Status,
                0x01
            );
            Packet.Register<PongResponse>(signature, Deserialize);
        }
        
        public required long Data { get; init; }
        
        protected override void Serialize(Stream stream, object registry)
        {
            stream.WriteS64(Data);
        }
        private static Packet Deserialize(Stream stream, object registry)
        {
            throw new NotImplementedException();
        }
    }
}