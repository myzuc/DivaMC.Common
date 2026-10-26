using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Protocol.Packets.Configuration.Serverbound
{
    public sealed record PingResponsePacket : Packet
    {
        static PingResponsePacket()
        {
            PacketSignature signature = new PacketSignature(
                ProtocolDirection.Serverbound,
                ProtocolStage.Configuration,
                0x05
            );
            Packet.Register<PingResponsePacket>(signature, Deserialize);
        }
        
        public required int Id { get; set; }
        
        protected override void Serialize(Stream stream)
        {
            throw new NotImplementedException();
        }
        private static Packet Deserialize(Stream stream)
        {
            return new PingResponsePacket()
            {
                Id = stream.ReadS32(),
            };
        }
    }
}