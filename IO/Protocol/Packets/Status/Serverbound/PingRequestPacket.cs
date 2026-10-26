using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Protocol.Packets.Status.Serverbound
{
    public sealed record PingRequestPacket : Packet
    {
        static PingRequestPacket()
        {
            PacketSignature signature = new PacketSignature(
                ProtocolDirection.Serverbound,
                ProtocolStage.Status,
                0x01
            );
            Packet.Register<PingRequestPacket>(signature, Deserialize);
        }
        
        public required long Data { get; set; }
        
        protected override void Serialize(Stream stream)
        {
            throw new NotImplementedException();
        }
        private static Packet Deserialize(Stream stream)
        {
            return new PingRequestPacket()
            {
                Data = stream.ReadS64(),
            };
        }
    }
}