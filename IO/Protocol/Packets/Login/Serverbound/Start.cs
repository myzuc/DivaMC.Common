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
        
        public required string Name { get; set; }
        public required Guid Id { get; set; }
        
        protected override void Serialize(Stream stream, Context context)
        {
            throw new NotImplementedException();
        }
        private static Packet Deserialize(Stream stream, Context context)
        {
            return new Start()
            {
                Name = stream.ReadT0AVS32(),
                Id = stream.ReadGuid(),
            };
        }
    }
}