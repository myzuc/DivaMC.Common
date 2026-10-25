using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Protocol.Packets.Handshake.Serverbound
{
    public sealed record Intention : Packet
    {
        static Intention()
        {
            PacketSignature signature = new PacketSignature(
                ProtocolDirection.Serverbound,
                ProtocolStage.Handshake,
                0x00
            );
            Packet.Register<Intention>(signature, Deserialize);
        }
        
        public required int Version { get; set; }
        public required string ServerAddress { get; set; }
        public required ushort ServerPort { get; set; }
        public required Intent Intent { get; set; }
        
        protected override void Serialize(Stream stream, object registry)
        {
            throw new NotImplementedException();
        }
        private static Packet Deserialize(Stream stream, object registry)
        {
            return new Intention()
            {
                Version = stream.ReadVS32(),
                ServerAddress = stream.ReadT0AVS32(),
                ServerPort = stream.ReadU16(),
                Intent = (Intent)stream.ReadVS32()
            };
        }
    }
}