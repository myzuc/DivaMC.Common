using Me.Shiokawaii.IO;

namespace Net.Myzuc.DivaMC.Common.IO.Protocol.Packets.Handshake.Serverbound
{
    public sealed record Intention : IPacket
    {
        static Intention()
        {
            Packet.Register<Intention>();
        }
        public static PacketSignature Signature => 
            new PacketSignature(
                ProtocolDirection.Serverbound,
                ProtocolStage.Handshake,
                0x00
            );
        
        public required int Version { get; init; }
        public required string ServerAddress { get; init; }
        public required ushort ServerPort { get; init; }
        public required Intent Intent { get; init; }
        
        public void Serialize(BinaryStream stream, object registry)
        {
            throw new NotImplementedException();
        }
        public static IPacket Deserialize(BinaryStream stream, object registry)
        {
            return new Intention()
            {
                Version = stream.ReadVS32(),
                ServerAddress = stream.ReadT0AU16(),
                ServerPort = stream.ReadU16(),
                Intent = (Intent)stream.ReadVS32()
            };
        }
    }
}