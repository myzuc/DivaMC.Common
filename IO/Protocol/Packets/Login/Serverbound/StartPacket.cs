using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Protocol.Packets.Login.Serverbound
{
    public sealed record StartPacket : Packet
    {
        static StartPacket()
        {
            PacketSignature signature = new PacketSignature(
                ProtocolDirection.Serverbound,
                ProtocolStage.Login,
                0x00
            );
            Packet.Register<StartPacket>(signature, Deserialize);
        }
        
        public required string Name { get; set; }
        public required Guid Id { get; set; }
        
        protected override void Serialize(Stream stream)
        {
            throw new NotImplementedException();
        }
        private static Packet Deserialize(Stream stream)
        {
            return new StartPacket()
            {
                Name = stream.ReadT0AVS32(),
                Id = stream.ReadGuid(),
            };
        }
    }
}