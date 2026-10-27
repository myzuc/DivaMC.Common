using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Protocol.Packets.Handshake
{
    public sealed class SBHandshakePacket : Packet
    {
        static SBHandshakePacket()
        {
            Register<SBHandshakePacket>(new(ProtocolDirection.Serverbound, ProtocolStage.Handshake,0x00), Deserialize);
        }
        
        public required int Version { get; set; }
        public required string ServerAddress { get; set; }
        public required ushort ServerPort { get; set; }
        public required Intent Intent { get; set; }
        
        public override void Serialize(Stream stream, PacketContext context)
        {
            throw new NotImplementedException();
        }
        new private static Packet Deserialize(Stream stream, PacketContext context)
        {
            return new SBHandshakePacket()
            {
                Version = stream.ReadVS32(),
                ServerAddress = stream.ReadT0AVS32(),
                ServerPort = stream.ReadU16(),
                Intent = (Intent)stream.ReadVS32()
            };
        }
    }
}