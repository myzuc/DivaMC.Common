using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Protocol.Packets.Handshake.Serverbound
{
    public sealed class HandshakePacket : Packet
    {
        static HandshakePacket()
        {
            Register<HandshakePacket>(Signature, Deserialize);
        }
        
        new public static readonly PacketSignature Signature = new PacketSignature(
            ProtocolDirection.Serverbound,
            ProtocolStage.Handshake,
            0x00
        );
        
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
            context.Check(Signature);
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