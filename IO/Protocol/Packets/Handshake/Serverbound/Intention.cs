namespace Net.Myzuc.DivaMC.Common.IO.Protocol.Packets.Handshake.Serverbound
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
        
        public required int Version { get; init; }
        public required string ServerAddress { get; init; }
        public required ushort ServerPort { get; init; }
        public required Intent Intent { get; init; }
        
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