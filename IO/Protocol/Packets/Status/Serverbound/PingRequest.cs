namespace DivaMC.Common.IO.Protocol.Packets.Status.Serverbound
{
    public sealed record PingRequest : Packet
    {
        static PingRequest()
        {
            PacketSignature signature = new PacketSignature(
                ProtocolDirection.Serverbound,
                ProtocolStage.Status,
                0x01
            );
            Packet.Register<PingRequest>(signature, Deserialize);
        }
        
        public required long Data { get; init; }
        
        protected override void Serialize(Stream stream, object registry)
        {
            throw new NotImplementedException();
        }
        private static Packet Deserialize(Stream stream, object registry)
        {
            return new PingRequest()
            {
                Data = stream.ReadS64(),
            };
        }
    }
}