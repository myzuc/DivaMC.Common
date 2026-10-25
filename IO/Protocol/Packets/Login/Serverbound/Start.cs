using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Protocol.Packets.Login.Serverbound
{
    public sealed record Start : Packet
    {
        static Start()
        {
            PacketSignature signature = new PacketSignature(
                ProtocolDirection.Serverbound,
                ProtocolStage.Login,
                0x00
            );
            Packet.Register<Start>(signature, Deserialize);
        }
        
        public required string Name { get; init; }
        public required Guid Id { get; init; }
        
        protected override void Serialize(Stream stream, object registry)
        {
            throw new NotImplementedException();
        }
        private static Packet Deserialize(Stream stream, object registry)
        {
            return new Start()
            {
                Name = stream.ReadT0AVS32(),
                Id = stream.ReadGuid(),
            };
        }
    }
}