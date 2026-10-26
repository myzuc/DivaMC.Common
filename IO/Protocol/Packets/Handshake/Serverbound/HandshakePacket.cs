using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Protocol.Packets.Handshake.Serverbound
{
    public sealed record HandshakePacket : Packet
    {
        static HandshakePacket()
        {
            PacketSignature signature = new PacketSignature(
                ProtocolDirection.Serverbound,
                ProtocolStage.Handshake,
                0x00
            );
            Packet.Register<HandshakePacket>(signature, Deserialize);
        }
        
        public required int Version { get; set; }
        public required string ServerAddress { get; set; }
        public required ushort ServerPort { get; set; }
        public required Intent Intent { get; set; }
        
        protected override void Serialize(Stream stream)
        {
            throw new NotImplementedException();
        }
        private static Packet Deserialize(Stream stream)
        {
            return new HandshakePacket()
            {
                Version = stream.ReadVS32(),
                ServerAddress = stream.ReadT0AVS32(),
                ServerPort = stream.ReadU16(),
                Intent = (Intent)stream.ReadVS32()
            };
        }
    }
}