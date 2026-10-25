using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Protocol.Packets.Configuration.Serverbound
{
    public sealed record PingResponse : Packet
    {
        static PingResponse()
        {
            PacketSignature signature = new PacketSignature(
                ProtocolDirection.Serverbound,
                ProtocolStage.Configuration,
                0x05
            );
            Packet.Register<PingResponse>(signature, Deserialize);
        }
        
        public required int Id { get; set; }
        
        protected override void Serialize(Stream stream, Context context)
        {
            throw new NotImplementedException();
        }
        private static Packet Deserialize(Stream stream, Context context)
        {
            return new PingResponse()
            {
                Id = stream.ReadS32(),
            };
        }
    }
}